using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Voxen.Server.Authentication.Interfaces;
using Voxen.Server.Authentication.Services;

namespace Voxen.Server.Authentication.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVoxenAuthentication(this IServiceCollection services,
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
