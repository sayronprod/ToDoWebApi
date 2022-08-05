using Microsoft.AspNetCore.DataProtection;
using ToDo.Data;
using ToDo.Models.Interfaces;

namespace ToDo.WebApi.Services
{
    public class SecureService : ISecureService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ApplicationSettings applicationSettings;
        private string Key => applicationSettings.SecretKey;

        public SecureService(IDataProtectionProvider dataProtectionProvider, ApplicationSettings applicationSettings)
        {
            _dataProtectionProvider = dataProtectionProvider;
            this.applicationSettings = applicationSettings;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}
