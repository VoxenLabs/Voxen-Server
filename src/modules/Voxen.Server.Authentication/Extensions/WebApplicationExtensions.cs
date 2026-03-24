using Microsoft.AspNetCore.Builder;

namespace Voxen.Server.Authentication.Extensions;

/// <summary>
/// Provides extension methods for configuring authentication and authorization in a <see cref="WebApplication"/>.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Configures the <see cref="WebApplication"/> to use authentication and authorization middleware.
    /// </summary>
    public static WebApplication UseVoxenAuthentication(this WebApplication app)
    {
        app
            .UseAuthentication()
            .UseAuthorization();

        return app;
    }
}
