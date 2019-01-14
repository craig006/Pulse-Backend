using System.Threading.Tasks;
using ServeUp.Models;

namespace ServeUp.Blocks
{
    public class AuthenticateUserBlock
    {
        public async Task<User> Begin(string identifier, string password) 
        {
            return await Task.FromResult(new User());
        }
    }
}