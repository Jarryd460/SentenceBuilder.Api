using HealthChecks.UI.Client;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    })
    // Besides the attributes placed on properties to add validation criteria, it reads
    // and adds fluent validation rules to swagger documentation
    .AddFluentValidationRulesToSwagger()
    // Add examples of requests to swagger documentation
    .AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

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
