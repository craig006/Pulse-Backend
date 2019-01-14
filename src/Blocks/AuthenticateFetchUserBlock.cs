using System.Security;
using System.Threading.Tasks;
using ServeUp.Models;
using ServeUp.Queries;

namespace ServeUp.Blocks
{
    public class AuthenticateFetchUserBlock
    {
        private readonly IFetchUserByContactDetailsQuery _fetchUserByContactDetailsQuery;

        public AuthenticateFetchUserBlock(IFetchUserByContactDetailsQuery fetchUserByContactDetailsQuery)
        {
            _fetchUserByContactDetailsQuery = fetchUserByContactDetailsQuery;
        }
    
        public async Task<User> Begin(int cellNumber, string email)
        {
            var user = await _fetchUserByContactDetailsQuery.Begin(cellNumber, email);

            if(user == null)
                throw new SecurityException();

            return user;
        }
    }
}