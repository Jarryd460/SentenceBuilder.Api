using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace SentenceBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformationController : ControllerBase
{
    [HttpGet("version", Name = nameof(GetVersion))]
    public ActionResult GetVersion()
    {
        return Ok(Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
    }
}
