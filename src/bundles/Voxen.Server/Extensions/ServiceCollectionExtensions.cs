using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Voxen.Server.Authentication.Extensions;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Entities;

namespace Voxen.Server.Extensions;

/// <summary>
/// Provides extension methods for configuring services in the application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures authentication services for the application, including Identity and JWT authentication.
    /// </summary>
    public static IServiceCollection AddVoxenAuthentication(this IServiceCollection services,
        IConfigurationSection jwtSettings)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<VoxenDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddJwtAuthentication(jwtSettings);
        services.AddAuthorization();

        return services;
    }

    /// <summary>
    /// Adds and configures API services for the Voxen Server application.
    /// </summary>
    public static IServiceCollection AddVoxenApiServices(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        services.AddFastEndpoints();
        services.SwaggerDocument(o =>
        {
            o.ShortSchemaNames = true;
            o.DocumentSettings = s =>
            {
                s.Title = "Voxen Server API";
                s.Version = "v1";
            };
        });

        return services;
    }
}
