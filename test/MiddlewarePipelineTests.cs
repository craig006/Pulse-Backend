using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.Repository;
using ServeUp.Stacks;
using ServeUp.System;
using Tests.Models;
using Xunit;

namespace test
{
    public class MiddlewarePipelineTests
    {
        ServiceProvider _serviceProvider;

        public MiddlewarePipelineTests()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<MyMiddleware>();

            collection.AddLogging();
            _serviceProvider = collection.BuildServiceProvider();
        }
        
        [Fact]
        public async void EnsureFinalGetsCalled_Success()
        {
            var pipeLine = new MiddlewarePipeLine(_serviceProvider);
            var middleware = _serviceProvider.GetService<MyMiddleware>();
            pipeLine.Use(middleware);

            var didRun = false;
            await pipeLine.Run(async () => { didRun = true; await Task.CompletedTask; });
            
            Assert.True(didRun);
        }

        [Fact]
        public async void EnsureMiddlewareGetsCalled_Success()
        {
            var pipeLine = new MiddlewarePipeLine(_serviceProvider);
            var middleware = _serviceProvider.GetService<MyMiddleware>();
            pipeLine.Use(middleware);

            await pipeLine.Run(async () => { await Task.CompletedTask; });
            
            Assert.True(middleware.DidRun);
        }

        [Fact]
        public async void EnsureMultipleMiddlewareGetsCalled_Success()
        {
            var pipeLine = new MiddlewarePipeLine(_serviceProvider);
            var middleware = _serviceProvider.GetService<MyMiddleware>();
            var middleware1 = _serviceProvider.GetService<MyMiddleware>();
            
            pipeLine.Use(middleware);
            pipeLine.Use(middleware1);
            
            await pipeLine.Run(async () => { await Task.CompletedTask; });
            
            Assert.True(middleware.DidRun);
            Assert.True(middleware1.DidRun);
        }

        [Fact]
        public async void EnsureMiddlewareRunOrder_Success()
        {
            var pipeLine = new MiddlewarePipeLine(_serviceProvider);
            var middleware = _serviceProvider.GetService<MyMiddleware>();
            var middleware1 = _serviceProvider.GetService<MyMiddleware>();
            
            pipeLine.Use(middleware);
            pipeLine.Use(middleware1);
            
            await pipeLine.Run(async () => { await Task.CompletedTask; });
            
            Assert.Equal(middleware.RanAtCount, 1);
            Assert.Equal(middleware1.RanAtCount, 2);
        }
    }
}