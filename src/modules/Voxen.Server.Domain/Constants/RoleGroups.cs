using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain.Constants;

/// <summary>
/// Provides a set of predefined role groups.
/// </summary>
public static class RoleGroups
{
    /// <summary>
    /// Represents a predefined role group that includes all roles defined in the <see cref="ServerRole"/> enumeration.
    /// </summary>
    public static readonly string Everyone = string.Join(",", Enum.GetNames<ServerRole>());
}
