using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace RapidBlazor.WebUI.Client.Authorization
{
    public class CustomAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
            : base(accessor)
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            var identity = (ClaimsIdentity)user.Identity!;

            if (account is not null)
            {
                foreach ((string? key, object? value) in account.AdditionalProperties)
                {
                    if (value is JsonElement { ValueKind: JsonValueKind.Array } element)
                    {
                        identity.RemoveClaim(identity.FindFirst(key));

                        var claims = element.EnumerateArray()
                            .Select(x => new Claim(key, x.ToString()));
                        
                        identity.AddClaims(claims);
                    }
                }
            }

            return user;
        }
    }
}
