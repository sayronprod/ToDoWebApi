using Microsoft.EntityFrameworkCore;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDbo;

namespace ToDo.Data.Repositorys
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext context;

        public RoleRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<RoleDbo>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }

        public async Task<RoleDbo?> GetRoleById(int id)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RoleDbo?> GetRoleByName(string name)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.RoleName == name);
        }

        public async Task<RoleDbo?> AddRole(RoleDbo role)
        {
            var createdRole = await context.Roles.AddAsync(role);

            await context.SaveChangesAsync();

            return createdRole.Entity;
        }

        public async Task DeleteRole(RoleDbo role)
        {
            context.Roles.Remove(role);
            await context.SaveChangesAsync();
        }
    }
}
