using Backend.Api.Controllers;
using Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Backend.Api.Tests;

public class DummiesControllerTests
{
    [Fact]
    public async Task Get_returns_the_sample_collection()
    {
        var controller = new DummiesController(new DummyService());

        var result = await controller.Get(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var items = Assert.IsAssignableFrom<IReadOnlyCollection<Backend.Models.DummyDbClass>>(okResult.Value);
        Assert.Collection(items,
            item => Assert.Equal("First sample item", item.POCODescription),
            item => Assert.Equal("Second sample item", item.POCODescription),
            item => Assert.Equal("Third sample item", item.POCODescription));
    }
}
