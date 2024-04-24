// ** library **
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
// ** architecture **
using locy_api.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using locy_api.Interfaces;

namespace locy_api.Helpers
{
    public class AuthHelpers: IAuthHelper
    {
        private readonly IConfiguration _config;
        private readonly CookieOptions _cookieOptions;
        private const string _KeyECBPKCS7 = "VSL@213$171$198&&17192125";

        public AuthHelpers(IConfiguration config)
        {
            _config = config;
            _cookieOptions = new CookieOptions()
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
            };
        }

        // Hàm mã hoá
        public string Encrypt(string textEncrypt)
        {
            var md5 = new MD5CryptoServiceProvider();

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_KeyECBPKCS7));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            var transform = tdes.CreateEncryptor();
            byte[] textBytes = Encoding.UTF8.GetBytes(textEncrypt);
            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);

            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        // Hàm giải mã
        public string Decrypt(string textDecrypt)
        {
            if (textDecrypt.Trim() == "") return "";
            var md5 = new MD5CryptoServiceProvider();

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(_KeyECBPKCS7));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            var transform = tdes.CreateDecryptor();
            byte[] cipherBytes = Convert.FromBase64String(textDecrypt);
            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(bytes);
        }

        // Hàm tạo token
        public string createToken(ProfileDto? profile)
        {
            if (profile == null) return "";

            // Get data from config file
            var jwtKey = _config["Jwt:Key"] ?? "";
            var jwtIssuer = _config["Jwt:Issuer"] ?? "";
            var jwtAudience = _config["Jwt:Audience"] ?? "";

            // Config jwt
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claim in jwt
            var claims = new[]
            {
               new Claim("Id", profile.Id.ToString()),
               new Claim("Permission", profile.permission ?? ""),
               new Claim("IDEmployee", profile.IDNhanVien.ToString() ?? "0"),
            };

            // Token option
            var tokenOptions = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public CookieOptions GetCookieOptions()
        {
            return _cookieOptions;
        }
    }
}
