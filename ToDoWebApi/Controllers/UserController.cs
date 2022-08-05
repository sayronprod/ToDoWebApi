using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDto;

namespace ToDo.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseApiController
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> Register(RegisterRequestDto request)
        {
            object result;
            User? createdUser = await userService.CreateUser(request.Login, request.Password);
            if (createdUser is null)
            {
                result = new MessageDto
                {
                    Message = $"User with login {request.Login} already exist"
                };
                SetStatusCode(400);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(201);
            }
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> UpdatePassword(UpdatePasswordRequestDto request)
        {
            object result;
            bool updatedResult = await userService.UpdateUserPassword(request.Login, request.OldPassword, request.NewPassword);
            if (updatedResult)
            {
                result = new MessageDto
                {
                    Message = "Error password update"
                };
                SetStatusCode(400);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        public async Task<ICollection<User>> GetUsers()
        {
            var users = await userService.GetUsers();
            return users;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> GetUserById(int id)
        {
            object result;
            var user = await userService.GetUserById(id);
            if (user is null)
            {
                result = new MessageDto
                {
                    Message = "User not found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = user;
                SetStatusCode(200);
            }
            return result;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> GetUserByLogin(string login)
        {
            object result;
            var user = await userService.GetUserByLogin(login);
            if (user is null)
            {
                result = new MessageDto
                {
                    Message = "User not found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = user;
                SetStatusCode(200);
            }
            return result;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> DeleteUserById(int id)
        {
            object result;
            bool deleteResult = await userService.DeleteUserById(id);
            if (!deleteResult)
            {
                result = new MessageDto
                {
                    Message = "User not found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(404, Type = typeof(MessageDto))]
        public async Task<object> DeleteUserByLogin(string login)
        {
            object result;
            bool deleteResult = await userService.DeleteUserByLogin(login);
            if (!deleteResult)
            {
                result = new MessageDto
                {
                    Message = "User not found"
                };
                SetStatusCode(404);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> GrantRoleToUserById(GrantRoleToUserByIdRequestDto request)
        {
            object result;
            (bool, string) grantResult = await userService.GrantRoleToUserById(request.UserId, request.RoleName);
            if (!grantResult.Item1)
            {
                result = new MessageDto
                {
                    Message = grantResult.Item2
                };
                SetStatusCode(400);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> GrantRoleToUserByLogin(GrantRoleToUserByLoginRequestDto request)
        {
            object result;
            (bool, string) grantResult = await userService.GrantRoleToUserByLogin(request.UserLogin, request.RoleName);
            if (!grantResult.Item1)
            {
                result = new MessageDto
                {
                    Message = grantResult.Item2
                };
                SetStatusCode(400);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> DeleteRoleInUserById(DeleteRoleInUserByIdRequestDto request)
        {
            object result;
            bool deleteResult = await userService.DeleteRoleInUserById(request.UserId, request.RoleName);
            if (!deleteResult)
            {
                result = new MessageDto
                {
                    Message = "Error"
                };
                SetStatusCode(400);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> DeleteRoleInUserByLogin(DeleteRoleInUserByLoginRequestDto request)
        {
            object result;
            bool deleteResult = await userService.DeleteRoleInUserByLogin(request.UserLogin, request.RoleName);
            if (!deleteResult)
            {
                result = new MessageDto
                {
                    Message = "Error"
                };
                SetStatusCode(400);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            return result;
        }
    }
}
