using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.Stacks;
using ServeUp.System;
using Xunit;

namespace test
{
    public class JwtServiceTests
    {
        ServiceProvider _serviceProvider;

        public JwtServiceTests()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<IPasswordHashService, PasswordHashService>();
            collection.AddTransient<UpdateUserPasswordHashBlock>();
            collection.AddLogging();
            _serviceProvider = collection.BuildServiceProvider();
        }
        
        // [Fact]
        // public void First()
        // {
        //     string algorithm = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
        //     string digest = "http://www.w3.org/2001/04/xmlenc#sha256";
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var symmetricKey = Encoding.UTF8.GetBytes("dHXjoKvOIKAz2RtanSYRQv2k3i1cF7rq");
        //     var securityKey = new SymmetricSecurityKey(symmetricKey);
        //     var credentials =new SigningCredentials(securityKey, algorithm, digest);

        //     var now = DateTime.UtcNow;
        //     var tokenDescriptor = new SecurityTokenDescriptor();

        //     tokenDescriptor.Subject = new ClaimsIdentity(new Claim[]
        //                 {
        //                     new Claim(ClaimTypes.Name, "Pedro"),
        //                     new Claim(ClaimTypes.Role, "Author"), 
        //                 });

        //     tokenDescriptor.SigningCredentials = credentials;


        //     var token = tokenHandler.CreateToken(tokenDescriptor);
            
        //     var tokenString = tokenHandler.WriteToken(token);

        //     Console.WriteLine(tokenString);
            
        //     var symmetricKey1 = Encoding.UTF8.GetBytes("dHXjoKvOIKAz2RtanSYRQv2k3i1cF7ra");
        //     var securityKey1 = new SymmetricSecurityKey(symmetricKey1);
        //     var credentials1 =new SigningCredentials(securityKey1, algorithm, digest);

        //     var validationParameters = new TokenValidationParameters()
        //         {
        //             ValidateIssuer = false,
        //             ValidateAudience = false,
        //             IssuerSigningKey = securityKey1

        //         };

            
        //     SecurityToken token1;
        //     var principal = tokenHandler.ValidateToken(tokenString, validationParameters, out token1);

        //     Assert.True(principal.Identities.First().Claims.Any(c => c.Type == ClaimTypes.Name && c.Value == "Pedro"));
        //     Assert.True(principal.Identities.First().Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Author"));
        // }
        
    }
}