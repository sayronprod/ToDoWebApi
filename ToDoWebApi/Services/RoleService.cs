using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDbo;

namespace ToDo.WebApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<ICollection<string>> GetRoles()
        {
            var roles = await roleRepository.GetRoles();
            return roles.Select(x => x.RoleName).ToList();
        }

        public async Task<bool> AddRole(string roleName)
        {
            bool result = false;
            var existRole = await roleRepository.GetRoleByName(roleName);
            if (existRole is null)
            {
                var newRole = new RoleDbo
                {
                    RoleName = roleName
                };
                await roleRepository.AddRole(newRole);
            }
            result = true;
            return result;
        }

        public async Task<bool> DeleteRole(string roleName)
        {
            bool result = false;
            var existRole = await roleRepository.GetRoleByName(roleName);
            if (existRole is not null)
            {
                await roleRepository.DeleteRole(existRole);
            }
            result = true;
            return result;
        }

        public async Task<bool> IsExistsRole(string roleName)
        {
            var roles = await GetRoles();
            return roles.Contains(roleName);
        }
    }
}
