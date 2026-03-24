namespace Voxen.Server.Channels.Endpoints.DeleteChannel;

/// <summary>
/// Represents a request to delete a channel identified by its unique identifier.
/// </summary>
public class DeleteChannelRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}
