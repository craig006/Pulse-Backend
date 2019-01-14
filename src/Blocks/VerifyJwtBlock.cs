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
    public class VerifyJwtBlock
    {
        private readonly ITokenService _tokenService;

        public VerifyJwtBlock(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        // public async Task<Identity> Begin(string token)
        // {
        //     var claimsIdentity = _tokenService.Validate(token);

        //     if(claimsIdentity !=)
        // }
    }
}