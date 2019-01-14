using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ServeUp.System
{
    public class MiddlewarePipeLine
    {
        private List<IMiddleWare> _middleware = new List<IMiddleWare>();
        private readonly IServiceProvider _serviceProvider;

        public MiddlewarePipeLine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int Count { get => _middleware.Count; }

        public void Use(IMiddleWare middleWare)
        {
            if(middleWare == null)
                return; 
            
            _middleware.Add(middleWare);
        }

        public void Use<M>() where M : IMiddleWare
        {
            Use(_serviceProvider.GetService<M>());
        }

        public async Task Run(Func<Task> final)
        {
            var next = final;

            for(int i = Count - 1; i >= 0; i--)
            {
                var ware = _middleware[i];
                ware.Next = next;
                next = ware.Invoke;
            }

            await _middleware[0].Invoke();
        }
    }

}

