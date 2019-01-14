using System.Threading.Tasks;

namespace ServeUp.System
{
    public interface IBeginnableReturn<T, TResult>
    {
        Task<TResult> Begin(T input);
    }

    public interface IBeginnableReturn<TResult>
    {
        Task<TResult> Begin();
    }
}

