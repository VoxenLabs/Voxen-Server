namespace Voxen.Server.Interfaces;

public interface IServerConfigurationProvider
{
    public Task<Entities.Server> GetAsync(CancellationToken ct = default);
}
