using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Voxen.Server.Authentication.Interfaces;
using Voxen.Server.Entities;

namespace Voxen.Server.Endpoints.Users.Login;

/// <summary>
/// Represents the endpoint responsible for handling user login requests.
/// </summary>
public class LoginEndpoint(UserManager<User> userManager, IJwtTokenService jwtService) : Endpoint<LoginRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    /// <inheritdoc />
    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            await Send.UnauthorizedAsync(cancellation: ct);
            return;
        }

        var token = jwtService.CreateToken(user.Id, user.UserName!, user.Role.ToString());

        await Send.OkAsync(new LoginResponse
        {
            AccessToken = token
        }, ct);
    }
}
