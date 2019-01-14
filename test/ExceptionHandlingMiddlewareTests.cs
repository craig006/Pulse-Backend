using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.Repository;
using ServeUp.Stacks;
using ServeUp.System;
using Xunit;

namespace test
{
    public class ExceptionHandlingMiddlewareTests
    {
        ServiceProvider _serviceProvider;

        public ExceptionHandlingMiddlewareTests()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ExecutionContext>();
            collection.AddLogging();
            collection.AddTransient<ExceptionHandlingMiddleware>();
            _serviceProvider = collection.BuildServiceProvider();

            var loggerFactory = _serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Information);
            loggerFactory.AddDebug(LogLevel.Information);
        }
        
        [Fact]
        public async void EsureExceptionPeggedToContext()
        {
            var pipeLine = new MiddlewarePipeLine(_serviceProvider);

            pipeLine.Use<ExceptionHandlingMiddleware>();

            await pipeLine.Run(async () => { await Task.CompletedTask; throw new Exception(); });

            var context = _serviceProvider.GetService<ExecutionContext>();
            
            Assert.NotNull(context.Exception);
        }
    }
}