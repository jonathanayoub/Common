using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Common.Web.Api.Auth
{
    public class JwtAuthenticateFilter : IAuthenticationFilter
    {
        private readonly IJwtParser _jwtParser;

        public bool AllowMultiple => false;
        public string Realm { get; set; }

        public JwtAuthenticateFilter(IJwtParser jwtParser)
        {
            Guard.NotNull(() => jwtParser, jwtParser);
            _jwtParser = jwtParser;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            AuthenticateRequest(context);
        }

        private void AuthenticateRequest(HttpAuthenticationContext context)
        {
            Guard.NotNull(() => context, context);
            var authenticateAttribute = context
                .ActionContext
                .ActionDescriptor
                .GetCustomAttributes<CustomAuthorizeAttribute>()
                .SingleOrDefault();

            if (authenticateAttribute == null)
                return;

            if (!VerifyRequestFormat(context))
                return;

            Authenticate(context, authenticateAttribute);
        }

        private static bool VerifyRequestFormat(HttpAuthenticationContext context)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], request);
                return false;
            }
            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], request);
                return false;
            }
            return true;
        }

        private void Authenticate(HttpAuthenticationContext context, CustomAuthorizeAttribute authenticateAttribute)
        {
            var token = context.Request.Headers.Authorization.Parameter;
            var principal = ParseToken(token);

            if (principal == null)
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);

            var principalRoles = principal.Claims.First(c =>
                c.Type == ClaimTypes.Role).Value.Split(',');

            if (authenticateAttribute.RolesSplit.Length > 0
                && !authenticateAttribute.RolesSplit.Intersect(principalRoles).Any())
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            else
                context.Principal = principal;
        }

        private ClaimsPrincipal ParseToken(string token)
        {
            ClaimsPrincipal principle = _jwtParser.ParseToken(token, ConfigurationManager.AppSettings["TokenKey"]);
            var identity = principle?.Identity as ClaimsIdentity;

            if (identity == null)
                return null;

            if (!identity.IsAuthenticated)
                return null;

            return principle;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}
