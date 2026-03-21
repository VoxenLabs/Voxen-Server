using Microsoft.AspNetCore.Builder;
using Voxen.Server.Channels.Hubs;

namespace Voxen.Server.Channels.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseVoxenChannels(this WebApplication app)
    {
        app.MapHub<TextChat>("/channels/connect");

        return app;
    }
}
