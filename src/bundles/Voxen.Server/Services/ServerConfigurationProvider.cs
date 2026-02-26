using Microsoft.EntityFrameworkCore;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Services;

public class ServerConfigurationProvider(VoxenDbContext db) : IServerConfigurationProvider
{
    public async Task<Entities.Server> GetAsync(CancellationToken ct = default)
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
                "More than one ServerConfiguration record exists.");
        }

        return records[0];
    }
}
