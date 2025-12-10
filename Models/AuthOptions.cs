namespace GasField.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

 public class AuthOptions
    {
        public const string ISSUER = "Shelyakina"; // издатель токена
        public const string AUDIENCE = "ApiClient"; // потребитель токена
        const string KEY = "aF9@tL3#zQ8!Rm2$Xp7^eW5&vN1*oJ6?cK0+uB4~YdS9=GhT%jI2|ZrP<_MfE>";   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
