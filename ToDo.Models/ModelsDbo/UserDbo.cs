namespace ToDo.Models.ModelsDbo
{
    public class UserDbo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<UserRoleDbo> UserRoles { get; set; }
    }
}
