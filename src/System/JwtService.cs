using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace ServeUp.System
{
    public interface ITokenService
    {
        string Create(ClaimsIdentity identity);

        ClaimsIdentity Validate(string token);
    }

    public class JwtService: ITokenService
    {
        private string _algorithm = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
        private string _digest = "http://www.w3.org/2001/04/xmlenc#sha256";
        private string _key = "dHXjoKvOIKAz2RtanSYRQv2k3i1cF7rq";

        private string _audience = "api.serveup.com";
        private string _issuer = "api.serveup.com";

        public string Create(ClaimsIdentity identity)
        {
            var securityKey = GetSecurityKey();
            var credentials = new SigningCredentials(securityKey, _algorithm, _digest);

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = identity;
            tokenDescriptor.Issuer = _issuer;
            tokenDescriptor.Audience = _audience;
            tokenDescriptor.SigningCredentials = credentials;

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsIdentity Validate(string token)
        {
            var securityKey = GetSecurityKey();

            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = securityKey
            };

            SecurityToken validatedToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal.Identities.First();
        }

        private SymmetricSecurityKey GetSecurityKey()
        {
            var symmetricKey = Encoding.UTF8.GetBytes(_key);
            var securityKey = new SymmetricSecurityKey(symmetricKey);
            return securityKey;
        }
    }
}