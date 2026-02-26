using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Voxen.Server.Entities;
using Voxen.Server.Interfaces;

namespace Voxen.Server.Endpoints.Users.CreateUser;

/// <summary>
/// Represents an endpoint for creating a new user in the system.
/// </summary>
/// <remarks>
/// This endpoint handles the registration of a new user by creating a user entity
/// and associating it with a server. It requires the user to have the <see cref="ServerRole.Admin"/> role
/// to perform this operation. The endpoint is accessible via the POST method at the route "/auth/register".
/// </remarks>
public class CreateUserEndpoint(UserManager<User> userManager, IServerConfigurationProvider serverConfigurationProvider) : Endpoint<CreateUserRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/auth/register");
        Roles(nameof(ServerRole.Admin));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateUserRequest request, CancellationToken ct)
    {
        var server = await serverConfigurationProvider.GetAsync(ct);
        var user = new User
        {
            UserName = request.Username,
            Server = server,
            ServerId = server.Id,
            Role = ServerRole.Member
        };
        
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            AddError(result.Errors.First().Description);
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        await Send.OkAsync(cancellation: ct);
    }
}
