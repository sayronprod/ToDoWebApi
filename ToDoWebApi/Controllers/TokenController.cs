using Microsoft.AspNetCore.Mvc;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDto;

namespace ToDo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : BaseApiController
    {
        private readonly ILogger<TokenController> logger;
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public TokenController(ILogger<TokenController> logger, IUserService userService, ITokenService tokenService)
        {
            this.logger = logger;
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TokenResponseDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object?> Token(TokenRequestDto request)
        {
            object result;
            bool authResult = await userService.AuthorizeUser(request.Login, request.Password);

            if (authResult)
            {
                (string, DateTime) tokenResult = tokenService.GetToken(request.Login);
                result = new TokenResponseDto
                {
                    Token = tokenResult.Item1,
                    Expired = tokenResult.Item2
                };
                SetStatusCode(200);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Incorrect authorization data"
                };
                SetStatusCode(400);
            }

            return result;
        }
    }
}