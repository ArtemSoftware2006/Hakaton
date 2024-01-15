using Microsoft.IdentityModel.Tokens;
using Npgsql.Replication;
using System.Text;

namespace Hakiton
{
    public class AuthTokenOptions
    {
        public string ISSUER {get;}
        public string AUDIENCE {get;}
        public int LIFETIME {get;}
        private readonly IConfiguration _configuration;
        private readonly string KEY;
        public AuthTokenOptions(IConfiguration configuration)
        {
            _configuration = configuration;

            ISSUER = _configuration["Jwt:Issuer"];
            AUDIENCE = _configuration["Jwt:Audience"];
            LIFETIME = int.Parse(_configuration["Jwt:Lifetime"]);
            KEY = _configuration["Jwt:Secret"];
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
