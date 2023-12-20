using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.WebUi.Shared.AccessControl;
using RapidBlazor.WebUi.Shared.Authorization;
using RapidBlazor.WebUi.Shared.Common;

namespace RapidBlazor.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager, 
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u =>
            u.Id == userId);
        return user.UserName!;
    }

    public async Task<Result<string>> CreateUserAsync(string userName, string password)
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

    public async Task<IList<RoleDto>> GetRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles
            .ToListAsync(cancellationToken);

        var result = roles
            .Select(r => new RoleDto(r.Id, r.Name ?? string.Empty, r.Permissions))
            .OrderBy(r => r.Name)
            .ToList();

        return result;
    }

    public async Task UpdateRolePermissionsAsync(string roleId, Permissions permissions)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role != null)
        {
            role.Permissions = permissions;

            await _roleManager.UpdateAsync(role);
        }
    }

    public async Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .OrderBy(r => r.UserName)
            .Select(u => new UserDto(u.Id, u.UserName ?? string.Empty, u.Email ?? string.Empty))
            .ToListAsync(cancellationToken);
    }

    public async Task<UserDto> GetUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        Guard.Against.NotFound(id, user);

        var result = new UserDto(user.Id, user.UserName ?? string.Empty, user.Email ?? string.Empty);

        var roles = await _userManager.GetRolesAsync(user);

        result.Roles.AddRange(roles);

        return result;
    }

    public async Task UpdateUserAsync(UserDto updatedUser)
    {
        var user = await _userManager.FindByIdAsync(updatedUser.Id);

        Guard.Against.NotFound(updatedUser.Id, user);

        user.UserName = updatedUser.UserName;
        user.Email = updatedUser.Email;

        await _userManager.UpdateAsync(user);

        var currentRoles = await _userManager.GetRolesAsync(user);
        var addedRoles = updatedUser.Roles.Except(currentRoles).ToList();
        var removedRoles = currentRoles.Except(updatedUser.Roles).ToList();

        if (addedRoles.Any())
        {
            await _userManager.AddToRolesAsync(user, addedRoles);
        }

        if (removedRoles.Any())
        {
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }
    }

    public async Task CreateRoleAsync(RoleDto newRole)
    {
        var role = new ApplicationRole { Name = newRole.Name };

        await _roleManager.CreateAsync(role);
    }

    public async Task UpdateRoleAsync(RoleDto updatedRole)
    {
        var role = await _roleManager.FindByIdAsync(updatedRole.Id);

        Guard.Against.NotFound(updatedRole.Id, role);

        role.Name = updatedRole.Name;

        await _roleManager.UpdateAsync(role);
    }

    public async Task DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        Guard.Against.NotFound(roleId, role);

        await _roleManager.DeleteAsync(role);
    }
}
