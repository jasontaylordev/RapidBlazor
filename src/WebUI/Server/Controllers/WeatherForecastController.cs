using CleanArchitectureBlazor.Application.WeatherForecasts.Queries;
using CleanArchitectureBlazor.WebUI.Shared.WeatherForecasts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBlazor.WebUI.Server.Controllers;

[Authorize]
public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IList<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}