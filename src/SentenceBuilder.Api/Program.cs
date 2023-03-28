using Application;
using HealthChecks.UI.Client;
using HealthChecks.UI.Data;
using Hellang.Middleware.ProblemDetails;
using Infrastructure;
using Infrastructure.Persistence;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SentenceBuilder.Api;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Mime;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices(builder.Configuration);

builder.Services.AddControllers(options =>
{
    // Sets all endpoints to accept and produce only json (application/json)
    // The default formatter options.InputFormatters[0] (SystemTextJsonInputFormatter) still supports application/*.json and text/json by default
    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
    options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));

    // Find all the input json formatters (formatters for handling request/body data)
    var jsonInputFormatters = options.InputFormatters.Where(formatter => formatter.GetType() == typeof(SystemTextJsonInputFormatter));
    // Get the supported media types of the json formatters
    var jsonInputSupportedMediaTypes = jsonInputFormatters.Select(formatter => ((SystemTextJsonInputFormatter)formatter).SupportedMediaTypes);

    foreach (MediaTypeCollection supportedMediaType in jsonInputSupportedMediaTypes)
    {
        // Remove text/json and application/*+json as it is not allowed as part of the request body
        // text/json is automatically not allowed without removing it but application/*+json is not and requires it's removal
        // application/*+json comes up as a option when specifying the request body content type
        supportedMediaType.Remove("text/json");
        supportedMediaType.Remove("application/*+json");
    }

    // Remove the unnecessary formatters because we only need formatter for application/json
    options.OutputFormatters.RemoveType<StringOutputFormatter>();

    // Find all the output json formatters (formatters for handling response/body data)
    var jsonOutputFormatters = options.OutputFormatters.Where(formatter => formatter.GetType() == typeof(SystemTextJsonOutputFormatter));
    // Get the supported media types of the json formatters
    var jsonOutputSupportedMediaTypes = jsonOutputFormatters.Select(formatter => ((SystemTextJsonOutputFormatter)formatter).SupportedMediaTypes);

    foreach (MediaTypeCollection supportedMediaType in jsonOutputSupportedMediaTypes)
    {
        // Remove text/json and application/*+json as it is not allowed as part of the response body
        // Having ProducesAttribute added as a filter does not require the removal of text/json and application/*+json as media types but
        // to be consistent, it's best to remove it
        supportedMediaType.Remove("text/json");
        supportedMediaType.Remove("application/*+json");
        supportedMediaType.Add("application/problem+json");
    }

    // Returns a 406 Http status code if requested content type (accept header) is not application/json
    options.ReturnHttpNotAcceptable = true;
});
builder.Services
    // The ApiController attribute on all controllers formats helper responses such as NotFound, Ok, BadRequest to a ProblemDetails object
    // which follows the standard for http responses (IETF RFC 7807) however unhandled exceptions are not automatically formatted.
    // Using the library Hellang.Middleware.ProblemDetails formats unhandled exceptions as well
    .AddProblemDetails(options =>
    {
        options.IncludeExceptionDetails = (context, exception) => context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment();
    })
    .AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen(options =>
    {
        // Add general detail about the web api
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Sentence Builder Api",
            Version = "1",
            Description = "An Api to that provides words of various word types. The types are Noun, Verb, Adjective, Adverb, Pronoun, Preposition, Conjunction, Determiner, Exclamation. Once the sentence has been built up, the user can persist the sentence in the backend.",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact()
            {
                Name = "Jarryd Deane",
                Email = "jarryd.deane@gmail.com",
                Url = new Uri("https://twitter.com/DeaneJarryd")
            },
            License = new OpenApiLicense()
            {
                Name = "MIT License",
                Url = new Uri("https://example.com/license")
            }
        });

        options.ExampleFilters();

        // Instead of setting the name property on the route attribute for every method
        // We can do this globally
        options.CustomOperationIds(description =>
        {
            return description.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
        });

        // By default swagger flattens inheritance hierarchies, which means base class properties are duplicated on subtypes
        // In order to maintain the heirarchy, we can call UseAllForInheritance() or UseOneOfForPolymorphism
        options.UseAllOfForInheritance();

        // Generate and use xml comments to enhance the swagger/Open API documentation
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Application.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Domain.xml"));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Infrastructure.xml"));

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    })
    // Besides the attributes placed on properties to add validation criteria, it reads
    // and adds fluent validation rules to swagger documentation
    .AddFluentValidationRulesToSwagger()
    // Add examples of requests to swagger documentation
    .AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Displays the operation id/route name set on the controller endpoint ([Route(Name = "endpoint name")]) which is also the name openapi generator cli
        // will give the method when generating the SDK
        options.DisplayOperationId();
        // As the method suggests, displays the duration of the request
        options.DisplayRequestDuration();
    });

    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync().ConfigureAwait(false);
        await initialiser.SeedAsync().ConfigureAwait(false);
    }

    using (var scope = app.Services.CreateScope())
    {
        var healthChecksDb = scope.ServiceProvider.GetRequiredService<HealthChecksDb>();
        healthChecksDb.Database.Migrate();
    }
}

app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
    // Formats the response such that the UI can understand
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
// Add UI for health checks json
app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
