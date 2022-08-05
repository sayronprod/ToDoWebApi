namespace ToDo.Models.Interfaces
{
    public interface IRoleService
    {
        public Task<ICollection<string>> GetRoles();
        public Task<bool> AddRole(string roleName);
        public Task<bool> DeleteRole(string roleName);
        public Task<bool> IsExistsRole(string roleName);
    }
}
