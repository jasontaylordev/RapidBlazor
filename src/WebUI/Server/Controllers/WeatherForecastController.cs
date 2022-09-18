using CleanArchitectureBlazor.Application.WeatherForecasts.Queries;
using CleanArchitectureBlazor.WebUI.Shared.WeatherForecasts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBlazor.WebUI.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IList<WeatherForecast>> Get()
    {
        return await _mediator.Send(new GetWeatherForecastsQuery());
    }
}