using System.Threading.Tasks;

namespace ServeUp.System
{
    public interface IBeginnable<T>
    {
        Task Begin(T input);
    }

    public interface IBeginnable
    {
        Task Begin();
    }
}

