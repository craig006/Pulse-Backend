using System.Threading.Tasks;

namespace ServeUp.Queries
{
    public interface IUpsertQuery<T>
    {
        Task Upsert(T document);
    }

}