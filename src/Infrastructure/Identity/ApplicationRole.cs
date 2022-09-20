using CleanArchitectureBlazor.WebUI.Shared.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureBlazor.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
