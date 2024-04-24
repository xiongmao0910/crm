using locy_api.Models.DTOs;

namespace locy_api.Interfaces
{
    public interface IAuthHelper
    {
        public string Encrypt(string textEncrypt);
        public string Decrypt(string textDecrypt);
        public string createToken(ProfileDto? profile);
        public CookieOptions GetCookieOptions();
    }
}
