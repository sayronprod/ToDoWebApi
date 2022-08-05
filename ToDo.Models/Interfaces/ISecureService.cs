namespace ToDo.Models.Interfaces
{
    public interface ISecureService
    {
        public string Encrypt(string data);
        public string Decrypt(string data);
    }
}
