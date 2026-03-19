using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain.Constants;

public static class VoxenRoles
{
    public static readonly string Everyone = string.Join(",", Enum.GetNames(typeof(ServerRole)));
}
