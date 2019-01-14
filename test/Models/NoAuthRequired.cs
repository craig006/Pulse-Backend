using System.Threading.Tasks;
using ServeUp.System;

namespace Tests.Models
{
    public class NoAuthRequired : IBeginnable
    {
        public Task Begin()
        {
            return Task.CompletedTask;
        }
    }
}