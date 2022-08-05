using ToDo.Models.ModelsDbo;

namespace ToDo.Models.Interfaces
{
    public interface IRoleRepository
    {
        public Task<ICollection<RoleDbo>> GetRoles();
        public Task<RoleDbo?> GetRoleById(int id);
        public Task<RoleDbo?> GetRoleByName(string name);
        public Task<RoleDbo?> AddRole(RoleDbo role);
        public Task DeleteRole(RoleDbo role);
    }
}
