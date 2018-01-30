using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Common.Web.Api.Auth
{
    public class JwtParser : IJwtParser
    {
        public ClaimsPrincipal ParseToken(string token, string tokenKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                var symmetricKey = Encoding.UTF8.GetBytes(tokenKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "all",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new InMemorySymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
