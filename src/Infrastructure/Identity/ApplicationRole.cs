using RapidBlazor.WebUI.Shared.Authorization;
using Microsoft.AspNetCore.Identity;

namespace RapidBlazor.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
