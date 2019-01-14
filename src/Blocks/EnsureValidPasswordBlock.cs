using System.Threading.Tasks;
using ServeUp.System;

namespace ServeUp.Blocks
{
    public class EnsureValidPasswordBlock
    {
        private readonly IPasswordHashService _passwordHashService;

        public EnsureValidPasswordBlock(IPasswordHashService passwordHashService)
        {
            _passwordHashService = passwordHashService;
        }
    
        public void Begin(string password, string hashedPassword)
        {
            _passwordHashService.EnsureVerified(password, hashedPassword);
        }
    }
}