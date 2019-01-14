using System.Threading.Tasks;

namespace ServeUp.Queries
{
    public interface IFetchQuery<T>
    {
        Task<T> Fetch(string id);
    }

}