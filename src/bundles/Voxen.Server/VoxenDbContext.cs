using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Entities;

namespace Voxen.Server;

public class VoxenDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public VoxenDbContext(DbContextOptions<VoxenDbContext> options) : base(options) { }

    public DbSet<Entities.Server> Server => Set<Entities.Server>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasOne(u => u.Server)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.ServerId);

        builder.Entity<Channel>()
            .HasDiscriminator(c => c.Type)
            .HasValue<Channel>(ChannelType.Text)
            .HasValue<Channel>(ChannelType.Voice);

        builder.Entity<Message>()
            .HasOne(m => m.Channel)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChannelId);

        builder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId);
    }
}
