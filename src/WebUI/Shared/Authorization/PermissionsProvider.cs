namespace CleanArchitecture.WebUI.Shared.Authorization;

public static class PermissionsProvider
{
    public static List<Permissions> GetAll()
    {
        return Enum.GetValues(typeof(Permissions))
            .OfType<Permissions>()
            .ToList();
    }
}
