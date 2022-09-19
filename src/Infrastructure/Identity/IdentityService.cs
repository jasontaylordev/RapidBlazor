using CaWorkshop.Application.Common.Models;
using CleanArchitectureBlazor.Application.Common.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureBlazor.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u =>
            u.Id == userId);

        return user.UserName!;
    }

    public async Task<Result<string>> CreateUserAsync(
        string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return Result<string>.Success(user.Id);
        }

        return Result<string>.Failure(result.Errors.Select(e => e.Description));
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u =>
            u.Id == userId);

        if (user != null)
        {
            return await DeleteUserAsync(user);
        }

        return Result.Success();
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        return Result.Failure(result.Errors.Select(e => e.Description));
    }
}
