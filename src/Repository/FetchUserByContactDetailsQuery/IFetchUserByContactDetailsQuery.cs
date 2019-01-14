using System.Threading.Tasks;
using ServeUp.Models;

namespace ServeUp.Queries
{
    public interface IFetchUserByContactDetailsQuery
    {
        Task<User> Begin(int cellNumber, string email);
    }
}