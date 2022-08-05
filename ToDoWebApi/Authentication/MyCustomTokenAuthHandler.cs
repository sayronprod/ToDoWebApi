using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using ToDo.Domain;
using ToDo.Models;
using ToDo.Models.Interfaces;

namespace ToDo.WebApi.Authentication
{
    public class MyCustomTokenAuthHandler : AuthenticationHandler<MyCustomTokenAuthOptions>
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public MyCustomTokenAuthHandler(IOptionsMonitor<MyCustomTokenAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserService userService, ITokenService tokenService)
            : base(options, logger, encoder, clock)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
                return AuthenticateResult.Fail($"Missing Header For Token: {Options.TokenHeaderName}");

            var token = Request.Headers[Options.TokenHeaderName].ToString();

            string json = tokenService.DecryptToken(token);

            TokenModel? tokenModel = json.Deserialize<TokenModel>();

            if (tokenModel is null)
            {
                return AuthenticateResult.Fail("Token invalid");
            }

            if (tokenModel.Expired < DateTime.Now)
            {
                return AuthenticateResult.Fail("The token has expired");
            }

            var user = await userService.GetUserByLogin(tokenModel.UserLogin);

            if (user is null)
            {
                return AuthenticateResult.Fail("Unknown token");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim("id",user.Id.ToString())
            };

            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var id = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(id);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
