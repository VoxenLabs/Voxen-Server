using Microsoft.EntityFrameworkCore;
using Voxen.Server.Domain;
using Voxen.Server.Info.Interfaces;

namespace Voxen.Server.Info.Services;

/// <inheritdoc />
public class ServerConfigurationProvider(VoxenDbContext db) : IServerConfigurationProvider
{
    /// <inheritdoc />
    public async Task<Domain.Entities.Server> GetAsync(CancellationToken ct = default)
    {
        var records = await db.Server.ToListAsync(ct);

        if (records.Count == 0)
        {
            throw new InvalidOperationException(
                "ServerConfiguration has not been initialized.");
        }

        if (records.Count > 1)
        {
            throw new InvalidOperationException(
                "More than one ServerConfiguration records exists.");
        }

        return records[0];
    }
}
