using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hakiton
{
    public class AuthTokenOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient"; 
        const string KEY = "secret060606gfghdgdsfgfdgergsdf";  
        public const int LIFETIME = 10; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
