using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RapidBlazor.WebUI.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor.Services;
using RapidBlazor.WebUI.Client.Authorization;
using RapidBlazor.WebUI.Shared.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("RapidBlazor.WebUI.ServerAPI",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(
    sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("RapidBlazor.WebUI.ServerAPI"));

builder.Services
    .AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<CustomAccountClaimsPrincipalFactory>();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, FlexibleAuthorizationPolicyProvider>();

builder.Services.Scan(scan => scan
    .FromAssemblyOf<IWeatherForecastClient>()
    .AddClasses()
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddMudServices();

await builder.Build().RunAsync();
