using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;

namespace Voxen.Server.Domain;

/// <summary>
/// Represents the database context for the Voxen Server.
/// </summary>
public class VoxenDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VoxenDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the database context.</param>
    public VoxenDbContext(DbContextOptions<VoxenDbContext> options) : base(options) { }

    /// <summary>
    /// Gets the DbSet that provides access to Server entities in the database.
    /// </summary>
    public DbSet<Entities.Server> Server => Set<Entities.Server>();

    /// <summary>
    /// Gets the set of channels available in the database context.
    /// </summary>
    public DbSet<Channel> Channels => Set<Channel>();

    /// <summary>
    /// Gets the set of messages stored in the database.
    /// </summary>
    public DbSet<Message> Messages => Set<Message>();

    /// <summary>
    /// Gets the database set for audit logs.
    /// </summary>
    public DbSet<Audit> AuditLogs => Set<Audit>();

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

        builder.Entity<Audit>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Action)
                .HasConversion<string>();

            entity.Property(a => a.Category)
                .HasConversion<string>();

            entity.Property(a => a.ChangesJson)
                .HasColumnType("TEXT");

            entity.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
