using NetEscapades.EnumGenerators;

namespace RapidBlazor.WebUi.Shared.Authorization;

[Flags]
[EnumExtensions]
public enum Permissions
{
    None = 0,
    ViewRoles = 1,
    ManageRoles = 2,
    ViewUsers = 4,
    ManageUsers = 8,
    ConfigureAccessControl = 16,
    ViewAccessControl = 32,
    Counter = 64,
    Forecast = 128,
    Todo = 256,
    All = ~None
}
