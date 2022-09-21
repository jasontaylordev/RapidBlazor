using CleanArchitectureBlazor.WebUI.Shared.WeatherForecasts;

namespace CleanArchitectureBlazor.Application.WeatherForecasts.Queries;

public class GetWeatherForecastsQuery : IRequest<IList<WeatherForecast>>
{
}

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IList<WeatherForecast>>
{
    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger _logger;

    public GetWeatherForecastsQueryHandler(ILogger<GetWeatherForecastsQuery> logger)
    {
        _logger = logger;
    }

    public async Task<IList<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToList();

        return await Task.FromResult(result);
    }
}