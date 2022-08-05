using ToDo.Models.ModelsDbo;

namespace ToDo.Models.Interfaces
{
    public interface IUserRepository
    {
        public Task<ICollection<UserDbo>> GetUsers();
        public Task<UserDbo?> GetUserById(int id);
        public Task<UserDbo?> GetUserByLogin(string login);
        public Task<UserDbo?> Add(UserDbo user);
        public Task<UserDbo?> Update(UserDbo user);
        public Task DeleteUser(UserDbo user);
    }
}
