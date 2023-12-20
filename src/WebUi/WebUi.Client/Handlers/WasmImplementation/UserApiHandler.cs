using RapidBlazor.WebUi.Client.Handlers.Interfaces;
using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.WebUi.Client.Handlers.WasmImplementation;

internal class UserApiHandler : IUserHandler
{
    private readonly IUsersClient _usersClient;

    public UserApiHandler(IUsersClient usersClient)
    {
        _usersClient = usersClient;
    }

    public Task<UserDetailsVm> GetUserAsync(string userId)
    {
        return _usersClient.GetUserAsync(userId);
    }

    public Task PutUserAsync(string userId, UserDto user)
    {
        return _usersClient.PutUserAsync(userId, user);
    }
}
