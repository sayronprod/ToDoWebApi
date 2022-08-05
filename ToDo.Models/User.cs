namespace ToDo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<string> UserRoles { get; set; }
    }
}
