using System.Threading.Tasks;

namespace ServeUp.Queries
{
    public interface IDeleteQuery
    {
        Task Delete(string id);
    }

}