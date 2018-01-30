using System.Security.Claims;

namespace Common.Web.Api.Auth
{
    public interface IJwtParser
    {
        ClaimsPrincipal ParseToken(string token, string tokenKey);
    }
}
