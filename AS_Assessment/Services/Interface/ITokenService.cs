using AS_Assessment.Models;
using Microsoft.AspNetCore.Identity;

namespace AS_Assessment.Services.Interface
{
    public interface ITokenService
    {
        string CreateAccessToken(IdentityUser user);
        RefreshToken GenerateRefreshToken();
    }
}
