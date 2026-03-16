namespace Voxen.Server.Entities;

/// <summary>
/// Represents the server configuration.
/// Only one server exists per Voxen instance.
/// </summary>
public class Server
{
    /// <summary>
    /// Gets or sets the unique identifier for the server.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the server.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the server was created (stored in UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Raw server logo bytes.
    /// </summary>
    public byte[]? Logo { get; set; }

    /// <summary>
    /// MIME type for the logo.
    /// </summary>
    public string? LogoContentType { get; set; }
}

