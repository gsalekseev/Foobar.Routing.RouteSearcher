﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Dto;
using Mixvel.Routing.RouteSearcher.Application.Business.Routes.Queries.SearchRoutes;

namespace Mixvel.Routing.RouteSearcher.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RouteController : ControllerBase
{
    private readonly IMediator mediator;

    public RouteController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchRoutesResultDto))]
    public async Task<IActionResult> Search([FromBody] SearchRoutesQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
