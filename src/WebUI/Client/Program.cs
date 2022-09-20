using CleanArchitectureBlazor.WebUI.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("CleanArchitectureBlazor.WebUI.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CleanArchitectureBlazor.WebUI.ServerAPI"));

builder.Services.AddApiAuthorization();

builder.Services.AddSingleton<IJSInProcessRuntime>(services =>
    (IJSInProcessRuntime)services.GetRequiredService<IJSRuntime>());

//builder.Services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>();
//builder.Services.AddHttpClient<ITodoListsClient, TodoListsClient>();
//builder.Services.AddHttpClient<ITodoItemsClient, TodoItemsClient>();

// NOTE: https://github.com/khellang/Scrutor/issues/180
builder.Services.Scan(scan => scan
    .FromAssemblyOf<IWeatherForecastClient>()
    .AddClasses()
    .AsImplementedInterfaces()
    .WithScopedLifetime());

await builder.Build().RunAsync();