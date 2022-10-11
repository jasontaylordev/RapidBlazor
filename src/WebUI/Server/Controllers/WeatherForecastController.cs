using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidBlazor.Application.WeatherForecasts.Queries;
using RapidBlazor.WebUI.Shared.WeatherForecasts;

namespace RapidBlazor.WebUI.Server.Controllers;

[Authorize]
public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IList<WeatherForecast>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
