using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mixvel.Routing.RouteSearcher.Api.Common;

[Route("api/[controller]")]
[ApiController]
public class RouteController : ControllerBase
{
    private readonly ProvidersConfiguration providersConfiguration;

    public RouteController(IOptions<ProvidersConfiguration> providersConfiguration)
    {
        var config = providersConfiguration.Value;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new {exception_message = ex.Message});
        }
    }
}
