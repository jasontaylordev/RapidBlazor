using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RapidBlazor.WebUi.Client.Components;

public class FlexibleAuthorizeView : AuthorizeView
{
    [Parameter, EditorRequired]
#pragma warning disable BL0007
    public Permissions Permissions
#pragma warning restore BL0007
    {
        get => string.IsNullOrEmpty(Policy) ? Permissions.None : PolicyNameHelper.GetPermissionsFrom(Policy);
        set => Policy = PolicyNameHelper.GeneratePolicyNameFor(value);
    }
}
