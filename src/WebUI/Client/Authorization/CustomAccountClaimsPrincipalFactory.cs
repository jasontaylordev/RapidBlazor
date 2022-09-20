using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace CleanArchitectureBlazor.WebUI.Client.Authorization;

public class CustomAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
        : base(accessor)
    {
    }

    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
        RemoteUserAccount account,
        RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        var identity = (ClaimsIdentity)user.Identity!;

        if (account != null)
        {
            foreach (var property in account.AdditionalProperties)
            {
                var key = property.Key;
                var value = property.Value;

                if (value != null &&
                    value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    identity.RemoveClaim(identity.FindFirst(property.Key));

                    var claims = element.EnumerateArray()
                        .Select(x => new Claim(property.Key, x.ToString()));

                    identity.AddClaims(claims);
                }
            }
        }

        return user;
    }
}
