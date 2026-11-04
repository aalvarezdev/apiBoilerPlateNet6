using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/version")]
public class VersionController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public ActionResult<BuildVersion> Get()
    {
        var commit = configuration["BUILD_COMMIT"] ?? "local";
        var buildNumber = configuration["BUILD_NUMBER"] ?? "local";
        return Ok(new BuildVersion(
            $"build-{buildNumber}-{commit[..System.Math.Min(7, commit.Length)]}",
            commit,
            buildNumber,
            configuration["BUILD_EXECUTION_ID"] ?? "local",
            configuration["BUILD_PIPELINE_URL"] ?? ""));
    }
}

public record BuildVersion(string Version, string Commit, string BuildNumber, string ExecutionId, string PipelineUrl);
