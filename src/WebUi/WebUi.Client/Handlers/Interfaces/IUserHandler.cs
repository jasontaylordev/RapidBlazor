using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.WebUi.Client.Handlers.Interfaces;

public interface IUserHandler
{
    Task<UserDetailsVm> GetUserAsync(string userId);
    Task PutUserAsync(string userId, UserDto user);
}
