using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain;

public class VoxenDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public VoxenDbContext(DbContextOptions<VoxenDbContext> options) : base(options) { }

    public DbSet<Entities.Server> Server => Set<Entities.Server>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<Message> Messages => Set<Message>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Channel>()
            .HasDiscriminator(c => c.Type)
            .HasValue<Channel>(ChannelType.Text)
            .HasValue<Channel>(ChannelType.Voice);

        builder.Entity<Message>()
            .HasOne(m => m.Channel)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
