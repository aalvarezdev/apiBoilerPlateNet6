using Backend.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Backend.Api.Tests;

public class VersionControllerTests
{
    [Fact]
    public void Get_returns_the_image_build_metadata()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["BUILD_COMMIT"] = "0123456789abcdef0123456789abcdef01234567",
            ["BUILD_NUMBER"] = "17",
            ["BUILD_EXECUTION_ID"] = "test-execution",
            ["BUILD_PIPELINE_URL"] = "https://example.com/pipeline/17"
        }).Build();

        var result = new VersionController(configuration).Get();

        var version = Assert.IsType<BuildVersion>(Assert.IsType<OkObjectResult>(result.Result).Value);
        Assert.Equal("build-17-0123456", version.Version);
        Assert.Equal(configuration["BUILD_COMMIT"], version.Commit);
        Assert.Equal("17", version.BuildNumber);
        Assert.Equal("test-execution", version.ExecutionId);
        Assert.Equal(configuration["BUILD_PIPELINE_URL"], version.PipelineUrl);
    }

    [Fact]
    public void Get_supports_local_development_without_build_metadata()
    {
        var result = new VersionController(new ConfigurationBuilder().Build()).Get();

        var version = Assert.IsType<BuildVersion>(Assert.IsType<OkObjectResult>(result.Result).Value);
        Assert.Equal("build-local-local", version.Version);
        Assert.Equal("local", version.Commit);
        Assert.Equal("", version.PipelineUrl);
    }
}
