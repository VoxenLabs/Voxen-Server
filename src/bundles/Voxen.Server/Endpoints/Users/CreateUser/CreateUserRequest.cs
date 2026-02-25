namespace Voxen.Server.Endpoints.Users.CreateUser;

public class CreateUserRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
