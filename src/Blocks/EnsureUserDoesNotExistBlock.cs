using System.Threading.Tasks;
using ServeUp.Models;
using ServeUp.Queries;
using ServeUp.System;

namespace ServeUp.Blocks
{
    public class EnsureUserDoesNotExistBlock
    {
        private readonly IFetchUserByContactDetailsQuery _fetchUserByContactDetailsQuery;

        public EnsureUserDoesNotExistBlock(IFetchUserByContactDetailsQuery fetchUserByContactDetailsQuery)
        {
            _fetchUserByContactDetailsQuery = fetchUserByContactDetailsQuery;
        }

        public async Task Begin(User user)
        {
            var existingUser = await _fetchUserByContactDetailsQuery.Begin(user.CellNumber, user.Email);

            if (existingUser != null)
            {
                throw new ConflictException("A user with these contact details already exists");
            }
        }
    }
}