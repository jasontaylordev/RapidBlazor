using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RapidBlazor.Domain.Entities;
using RapidBlazor.Infrastructure.Identity;
using RapidBlazor.WebUi.Shared.Authorization;

namespace RapidBlazor.Infrastructure.Data;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private const string? AdministratorsRole = "Administrators";
    private const string? AccountsRole = "Accounts";
    private const string? OperationsRole = "Operations";

    private const string DefaultPassword = "Password123!";

    public ApplicationDbContextInitializer(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        await InitialiseWithMigrationsAsync();
    }

    public async Task SeedAsync()
    {
        await SeedIdentityAsync();
        await SeedDataAsync();
    }

    private async Task InitialiseWithDropCreateAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    private async Task InitialiseWithMigrationsAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
        else
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

    private async Task SeedIdentityAsync()
    {
        await CreateRole(AdministratorsRole, Permissions.All);
        await CreateRole(AccountsRole, Permissions.ViewUsers | Permissions.Counter);
        await CreateRole(OperationsRole, Permissions.ViewUsers | Permissions.Forecast);

        await CreateUser("admin@localhost", new[] { AdministratorsRole });
        await CreateUser("auditor@localost", new[] { AccountsRole, OperationsRole });

        await _context.SaveChangesAsync();
    }

    private async Task CreateRole(string? roleName, Permissions permissions)
    {
        await _roleManager.CreateAsync(
            new ApplicationRole { Name = roleName, NormalizedName = roleName?.ToUpper(), Permissions = permissions });
    }

    private async Task CreateUser(string userName, IEnumerable<string?>? roles = null)
    {
        var user = new ApplicationUser { UserName = userName, Email = userName };

        await _userManager.CreateAsync(user, DefaultPassword);

        user = await _userManager.FindByNameAsync(userName);

        foreach (var role in roles?? Enumerable.Empty<string>())
        {
            if (!string.IsNullOrEmpty(role))
                await _userManager.AddToRoleAsync(user!, role);
        }
            
    }

    private async Task SeedDataAsync()
    {
        if (await _context.TodoLists.AnyAsync())
        {
            return;
        }

        var list = new TodoList
        {
            Title = "✨ Todo List",
            Items =
            {
                new TodoItem { Title = "Make a todo list 📃" },
                new TodoItem { Title = "Check off the first item ✅" },
                new TodoItem { Title = "Realise you've already done two things on the list! 🤯" },
                new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
            }
        };

        _context.TodoLists.Add(list);
        await _context.SaveChangesAsync();
    }
}
