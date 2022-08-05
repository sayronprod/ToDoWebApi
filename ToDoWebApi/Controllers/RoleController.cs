using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDto;

namespace ToDo.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseApiController
    {
        private readonly ILogger<UserController> logger;
        private readonly IRoleService roleService;

        public RoleController(ILogger<UserController> logger, IRoleService roleService)
        {
            this.logger = logger;
            this.roleService = roleService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<string>))]
        public async Task<ICollection<string>> GetRoles()
        {
            ICollection<string>? roles = await roleService.GetRoles();
            return roles;
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> AddRole(AddRoleRequestDto request)
        {
            object result;
            bool addResult = await roleService.AddRole(request.RoleName);
            if (addResult)
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Error"
                };
                SetStatusCode(400);
            }
            return result;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400, Type = typeof(MessageDto))]
        public async Task<object> DeleteRole(DeleteRoleRequestDto request)
        {
            object result;
            bool deleteResult = await roleService.DeleteRole(request.RoleName);
            if (deleteResult)
            {
                result = new MessageDto
                {
                    Message = "Success"
                };
                SetStatusCode(200);
            }
            else
            {
                result = new MessageDto
                {
                    Message = "Error"
                };
                SetStatusCode(400);
            }
            return result;
        }
    }
}
