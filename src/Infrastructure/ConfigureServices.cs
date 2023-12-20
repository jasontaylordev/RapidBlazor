using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RapidBlazor.Application.Common.Services.Data;
using RapidBlazor.Infrastructure.Data;
using RapidBlazor.Infrastructure.Data.Interceptors;
using RapidBlazor.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using RapidBlazor.Application.Common.Services.DateTime;
using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.Infrastructure.DateTime;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<ApplicationRole>()
            .AddSignInManager()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
