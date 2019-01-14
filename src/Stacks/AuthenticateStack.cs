using System.Threading.Tasks;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.System;

namespace ServeUp.Stacks
{
    public class AuthenticateStack: IBeginnableReturn<Credentials, string>
    {
        private readonly AuthenticateFetchUserBlock _authenticateFetchUserBlock;

        private readonly EnsureValidPasswordBlock _ensureValidPasswordBlock;

        private readonly GenerateJwtBlock _generateJwtBlock;

        public AuthenticateStack(AuthenticateFetchUserBlock authenticateFetchUserBlock, EnsureValidPasswordBlock ensureValidPasswordBlock, GenerateJwtBlock generateJwtBlock)
        {
            _generateJwtBlock = generateJwtBlock;
            _ensureValidPasswordBlock = ensureValidPasswordBlock;
            _authenticateFetchUserBlock = authenticateFetchUserBlock;
        }
    
        public async Task<string> Begin(Credentials credentials)
        {
            var user = await _authenticateFetchUserBlock.Begin(credentials.CellNumber, credentials.Email);

            _ensureValidPasswordBlock.Begin(credentials.Password, user.PasswordHash);

            return await _generateJwtBlock.Begin(user);
        }
    }
}