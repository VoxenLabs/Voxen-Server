namespace Voxen.Server.Endpoints.CreateUser;

public class CreateUserRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
