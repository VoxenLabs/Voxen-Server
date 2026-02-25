using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Voxen.Server.Entities;

namespace Voxen.Server.Endpoints.Users.CreateUser;

public class CreateUserEndpoint(UserManager<User> userManager) : Endpoint<CreateUserRequest>
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
        var user = new User { UserName = request.Username };
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
