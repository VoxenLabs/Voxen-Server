namespace Voxen.Server.Info.Interfaces;

/// <summary>
/// Provides functionality to retrieve server configuration details.
/// </summary>
/// <remarks>
/// This interface defines methods for accessing server configuration data, 
/// such as retrieving server details asynchronously. It is intended to be 
/// implemented by classes that interact with the underlying data storage 
/// to provide server configuration information.
/// </remarks>
public interface IServerConfigurationProvider
{
    /// <summary>
    /// Asynchronously retrieves the server configuration details.
    /// </summary>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the 
    /// <see cref="Server"/> instance representing the server configuration.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no server configuration exists or if multiple server configuration records are found.
    /// </exception>
    public Task<Domain.Entities.Server> GetAsync(CancellationToken ct = default);
}
