using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Voxen.Server.Authentication.Interfaces;
using Voxen.Server.Authentication.Services;
using Voxen.Server.Domain;
using Voxen.Server.Domain.Entities;

namespace Voxen.Server.Authentication.Extensions;

/// <summary>
/// Provides extension methods for configuring authentication services in the application.
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),

                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddAuthorization();

        return services;
    }
}
