using Microsoft.AspNetCore.Identity;
using RapidBlazor.WebUi.Shared.Authorization;

namespace RapidBlazor.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
