namespace Voxen.Server.Endpoints.Users.Login;

public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
