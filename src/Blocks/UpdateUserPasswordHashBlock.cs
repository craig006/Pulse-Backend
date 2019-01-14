using System.Threading.Tasks;
using ServeUp.Models;
using ServeUp.System;

namespace ServeUp.Blocks
{
    public class UpdateUserPasswordHashBlock
    {
        private readonly IPasswordHashService _hashService;

        public UpdateUserPasswordHashBlock(IPasswordHashService hashService)
        {
            _hashService = hashService;
        }

        public async Task Begin(string password, User user)
        {
            var passwordHash = _hashService.Hash(password);
            user.PasswordHash = passwordHash;
            await Task.CompletedTask;
        }
    }
}