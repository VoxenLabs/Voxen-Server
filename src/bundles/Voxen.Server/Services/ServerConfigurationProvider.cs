using Microsoft.EntityFrameworkCore;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Services;

public class ServerConfigurationProvider(VoxenDbContext db) : IServerConfigurationProvider
{
    /// <summary>
    /// Asynchronously retrieves the server configuration from the database.
    /// </summary>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the single 
    /// <see cref="Entities.Server"/> instance representing the server configuration.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no server configuration records exist in the database or if more than one 
    /// server configuration record is found.
    /// </exception>
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
