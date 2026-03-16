using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Voxen.Server.Domain.Entities;
using Voxen.Server.Domain.Enums;
using Voxen.Server.Info.Interfaces;

namespace Voxen.Server.Authentication.Endpoints.CreateUser;

/// <summary>
/// Represents an endpoint for creating a new user in the system.
/// </summary>
public class CreateUserEndpoint(UserManager<User> userManager, IServerConfigurationProvider serverConfigurationProvider) : Endpoint<CreateUserRequest>
{
    /// <inheritdoc />
    public override void Configure()
    {
        Post("/users/register");
        AllowAnonymous();
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
