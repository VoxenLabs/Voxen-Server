using Microsoft.AspNetCore.Builder;

namespace Voxen.Server.Authentication.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseVoxenAuthentication(this WebApplication app)
    {
        app
            .UseAuthentication()
            .UseAuthorization();

        return app;
    }
}
