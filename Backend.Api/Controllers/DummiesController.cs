using Backend.Application.Interfaces;
using Backend.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DummiesController(IDummyService dummyService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<DummyDbClass>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<DummyDbClass>>> Get(CancellationToken cancellationToken)
    {
        var items = await dummyService.GetDummyCollectionAsync();
        return Ok(items);
    }
}
