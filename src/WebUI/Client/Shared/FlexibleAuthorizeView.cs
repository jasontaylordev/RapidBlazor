using Microsoft.AspNetCore.Components;
using RapidBlazor.WebUI.Shared.Authorization;

namespace RapidBlazor.WebUI.Client.Shared;

public class FlexibleAuthorizeView : Microsoft.AspNetCore.Components.Authorization.AuthorizeView
{
    [Parameter]
#pragma warning disable BL0007 // Component parameters should be auto properties
    public Permissions Permissions
#pragma warning restore BL0007 // Component parameters should be auto properties
    {
        get
        {
            return string.IsNullOrEmpty(Policy) ? Permissions.None : PolicyNameHelper.GetPermissionsFrom(Policy);
        }
        set
        {
            Policy = PolicyNameHelper.GeneratePolicyNameFor(value);
        }
    }
}
