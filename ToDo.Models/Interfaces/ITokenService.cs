namespace ToDo.Models.Interfaces
{
    public interface ITokenService
    {
        public string DecryptToken(string token);
        public (string, DateTime) GetToken(string userLogin);
    }
}
