using Microsoft.AspNetCore.Builder;
using Voxen.Server.Channels.Hubs;

namespace Voxen.Server.Channels.Extensions;

/// <summary>
/// Provides extension methods for configuring the <see cref="WebApplication"/> to use Voxen Channels.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Configures the specified <see cref="WebApplication"/> to use Voxen Channels by mapping the <see cref="TextChat"/> hub to the "/channels/connect" endpoint.
    /// </summary>
    public static WebApplication UseVoxenChannels(this WebApplication app)
    {
        app.MapHub<TextChat>("/channels/connect");
        return app;
    }
}

