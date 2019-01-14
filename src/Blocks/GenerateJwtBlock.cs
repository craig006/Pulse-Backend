using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ServeUp.Models;
using ServeUp.System;

namespace ServeUp.Blocks
{
    [Authorize]
    public class GenerateJwtBlock
    {
        private readonly ITokenService _tokenService;

        public GenerateJwtBlock(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        public async Task<String> Begin(User user)
        {
            var idemtityClaims = user.Claims.ConvertAll(c => c.ToIdentityClaim());

            var claimsIdentity = new ClaimsIdentity(idemtityClaims, "Password");
            
            return await Task.FromResult(_tokenService.Create(claimsIdentity));
        }
    }
}