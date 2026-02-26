using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Voxen.Server.Authentication.Interfaces;
using Voxen.Server.Authentication.Services;

namespace Voxen.Server.Authentication.Extensions;

/// <summary>
/// Provides extension methods for configuring authentication services in the application.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures JWT-based authentication for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the authentication services will be added.</param>
    /// <param name="jwtSettings">
    /// A configuration section containing the JWT settings, including the signing key, issuer, and audience.
    /// </param>
    /// <returns>The updated <see cref="IServiceCollection"/> with authentication services configured.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfigurationSection jwtSettings)
    {
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ClockSkew = TimeSpan.Zero
                };
            });
        
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        return services;
    }
}
