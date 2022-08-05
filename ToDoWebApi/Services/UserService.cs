using AutoMapper;
using ToDo.Domain;
using ToDo.Models;
using ToDo.Models.Interfaces;
using ToDo.Models.ModelsDbo;

namespace ToDo.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IRoleService roleService;

        public UserService(IUserRepository userRepository, IMapper mapper, IRoleService roleService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.roleService = roleService;
        }

        public async Task<ICollection<User>> GetUsers()
        {
            var users = await userRepository.GetUsers();
            ICollection<User> result = mapper.Map<ICollection<User>>(users);
            return result;
        }

        public async Task<User?> GetUserById(int id)
        {
            User? result = null;

            var user = await userRepository.GetUserById(id);
            if (user is not null)
            {
                result = mapper.Map<User>(user);
            }

            return result;
        }

        public async Task<User?> GetUserByLogin(string login)
        {
            User? result = null;

            var user = await userRepository.GetUserByLogin(login);
            if (user is not null)
            {
                result = mapper.Map<User>(user);
            }

            return result;
        }

        public async Task<User?> CreateUser(string login, string password)
        {
            User? createdUser = null;
            var existUser = await userRepository.GetUserByLogin(login);

            if (existUser is null)
            {
                UserDbo newUser = new UserDbo
                {
                    Login = login,
                    PasswordHash = HashHelper.GetHash(password),
                    RegistrationDate = DateTime.Now
                };
                var createdResult = await userRepository.Add(newUser);

                createdUser = mapper.Map<User>(createdResult);
            }

            return createdUser;
        }

        public async Task<bool> UpdateUserPassword(string login, string oldPassword, string newPassword)
        {
            bool result = false;

            var user = await userRepository.GetUserByLogin(login);
            if (user is not null)
            {
                string oldPasswordHash = HashHelper.GetHash(oldPassword);
                if (user.PasswordHash == oldPasswordHash)
                {
                    string newPasswordHash = HashHelper.GetHash(newPassword);
                    user.PasswordHash = newPasswordHash;
                    await userRepository.Update(user);
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> DeleteUserById(int id)
        {
            bool result = false;

            var user = await userRepository.GetUserById(id);
            if (user is not null)
            {
                await userRepository.DeleteUser(user);
                result = true;
            }

            return result;
        }

        public async Task<bool> DeleteUserByLogin(string login)
        {
            bool result = false;

            var user = await userRepository.GetUserByLogin(login);
            if (user is not null)
            {
                await userRepository.DeleteUser(user);
                result = true;
            }

            return result;
        }

        public async Task<bool> AuthorizeUser(string login, string password)
        {
            bool result = false;

            var user = await userRepository.GetUserByLogin(login);
            if (user is not null)
            {
                string passwordHash = HashHelper.GetHash(password);
                if (user.PasswordHash == passwordHash)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<(bool, string)> GrantRoleToUserById(int id, string roleName)
        {
            bool result = false;
            string message = string.Empty;

            var user = await userRepository.GetUserById(id);
            if (user is not null)
            {
                bool isExistsRole = await roleService.IsExistsRole(roleName);
                if (isExistsRole)
                {
                    if (!user.UserRoles.Any(x => x.RoleName == roleName))
                    {
                        user.UserRoles.Add(new UserRoleDbo
                        {
                            RoleName = roleName,
                            UserId = user.Id
                        });
                        await userRepository.Update(user);
                    }
                    result = true;
                }
                else
                {
                    message = "Role not found";
                }
            }
            else
            {
                message = "User not found";
            }

            return (result, message);
        }

        public async Task<(bool, string)> GrantRoleToUserByLogin(string login, string roleName)
        {
            bool result = false;
            string message = string.Empty;

            var user = await userRepository.GetUserByLogin(login);
            if (user is not null)
            {
                bool isExistsRole = await roleService.IsExistsRole(roleName);
                if (isExistsRole)
                {
                    if (!user.UserRoles.Any(x => x.RoleName == roleName))
                    {
                        user.UserRoles.Add(new UserRoleDbo
                        {
                            RoleName = roleName,
                            UserId = user.Id
                        });
                        await userRepository.Update(user);
                    }
                    result = true;
                }
                else
                {
                    message = "Role not found";
                }
            }
            else
            {
                message = "User not found";
            }

            return (result, message);
        }

        public async Task<bool> DeleteRoleInUserById(int id, string roleName)
        {
            bool result = false;

            var user = await userRepository.GetUserById(id);
            if (user is not null)
            {
                var newRoles = user.UserRoles.Where(x => x.RoleName != roleName).ToList();
                user.UserRoles = newRoles;
                await userRepository.Update(user);
                result = true;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public async Task<bool> DeleteRoleInUserByLogin(string login, string roleName)
        {
            bool result = false;

            var user = await userRepository.GetUserByLogin(login);
            if (user is not null)
            {
                var newRoles = user.UserRoles.Where(x => x.RoleName != roleName).ToList();
                user.UserRoles = newRoles;
                await userRepository.Update(user);
                result = true;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
