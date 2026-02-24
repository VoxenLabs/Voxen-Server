using Microsoft.Extensions.Configuration;

namespace Voxen.Server.Authentication.Interfaces;

public interface IJwtTokenService
{
    public string CreateToken(Guid userId, string userName);
}
