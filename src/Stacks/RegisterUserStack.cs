using System.Threading.Tasks;
using ServeUp.Models;
using ServeUp.System;
using System.ComponentModel.DataAnnotations;
using ServeUp.Blocks;

namespace ServeUp.Stacks
{
    public class RegisterUserStack : IBeginnable<UserRegistration>
    {
        private readonly EnsureUserDoesNotExistBlock _ensureUserDoesNotExistBlock;
        private readonly UpdateUserPasswordHashBlock _updateUserPasswordHashBlock;
        private readonly SaveUserBlock _saveUserBlock;

        public RegisterUserStack(EnsureUserDoesNotExistBlock ensureUserDoesNotExistBlock, UpdateUserPasswordHashBlock updateUserPasswordHashBlock, SaveUserBlock saveUserBlock)
        {
            _saveUserBlock = saveUserBlock;
            _updateUserPasswordHashBlock = updateUserPasswordHashBlock;
            _ensureUserDoesNotExistBlock = ensureUserDoesNotExistBlock;
        }

        public async Task Begin(UserRegistration userRegistration)
        {
            await _ensureUserDoesNotExistBlock.Begin(userRegistration.User);

            await _updateUserPasswordHashBlock.Begin(userRegistration.Password, userRegistration.User);

            await _saveUserBlock.Begin(userRegistration.User);
        }
    }
}