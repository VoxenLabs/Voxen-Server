using Microsoft.AspNetCore.Identity;

namespace Voxen.Server.Entities;

public class User : IdentityUser<Guid>
{
    public Guid ServerId { get; set; }
    public Server Server { get; set; } = default!;

    public ServerRole Role { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

public enum ServerRole
{
    Member = 1,
    Admin = 2
}
