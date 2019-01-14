using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Flexi.Website.Utilities.Extentions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServeUp.Blocks;
using ServeUp.Database;
using ServeUp.Queries;
using ServeUp.Stacks;

namespace ServeUp.System
{
    public class DefualtPipeLine: Pipeline
    {
        public DefualtPipeLine(ILogger logger): base(logger)
        {
        }

        public override void ConfigureServices(ServiceCollection services)
        {
            services.AddTransientInNameSpace(typeof(Stack).Assembly, typeof(Stack).Namespace);
            services.AddTransientInNameSpace(typeof(Block).Assembly, typeof(Block).Namespace);
            services.AddTransient(typeof(IDeleteQuery), typeof(DeleteQuery));
            services.AddTransient(typeof(IFetchQuery<>), typeof(FetchQuery<>));
            services.AddTransient(typeof(IUpsertQuery<>), typeof(UpsertQuery<>));
            services.AddTransient<IFetchUserByContactDetailsQuery, FetchUserByContactDetailsQuery>();
            services.AddTransient<IFetchWaterUsageQuery, FetchWaterUsageQuery>();

            services.AddSingleton<DatabaseConnector>();
            services.AddApplicationServices();
            services.AddLogging();
        }

        public override void ConfigureMiddleware(MiddlewarePipeLine collection)
        {
            collection.Use(this.GetService<ExceptionHandlingMiddleware>());
            collection.Use(this.GetService<AuthorisationMiddleware>());
        }

        public override void ConfigureApplication()
        {
            var loggerFactory = this.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Information);
            loggerFactory.AddDebug(LogLevel.Information);
        }
    }

    public abstract class Pipeline : IServiceProvider
    {
        private MiddlewarePipeLine _middleWarePipeLine;
        public ExecutionContext Context { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }

        public Pipeline(ILogger logger)
        {
            Setup(logger);
        }

        private void Setup(ILogger logger) 
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<ILogger>(logger);
            ConfigureServices(collection);
            ServiceProvider =  collection.BuildServiceProvider();

            var middleware = new MiddlewarePipeLine(ServiceProvider);
            ConfigureMiddleware(middleware);
            _middleWarePipeLine = middleware;

            ConfigureApplication();

        }

        public abstract void ConfigureServices(ServiceCollection services);

        public abstract void ConfigureMiddleware(MiddlewarePipeLine collection);

        public abstract void ConfigureApplication();

        public async Task<TResult> ExecuteStack<TService, T, TResult>(T input) where TService : IBeginnableReturn<T, TResult>
        {
            var service = BeforeExecute<TService>();

            Context.Input = input;

            TResult result = default(TResult);

            Func<Task> execute = async () => {
                Context.Output = result = await service.Begin(input);
            };

            await _middleWarePipeLine.Run(execute);

            return result;
        }

        public async Task<TResult> ExecuteStack<TService, TResult>() where TService : IBeginnableReturn<TResult>
        {
            var service = BeforeExecute<TService>();
            
            TResult result = default(TResult);

            Func<Task> execute = async () => {
                Context.Output = result = await service.Begin();
            };

            await _middleWarePipeLine.Run(execute);

            return result;
        }

        public async Task ExecuteStack<TService, T>(T input) where TService : IBeginnable<T>
        {
            var service = BeforeExecute<TService>();

            Context.Input = input;

            await _middleWarePipeLine.Run(async () => { await service.Begin(input); });
        }

        public async Task ExecuteStack<TService>() where TService : IBeginnable
        {
            var service = BeforeExecute<TService>();

            await _middleWarePipeLine.Run(async () => { await service.Begin(); });
        }

        private TService BeforeExecute<TService>()
        {
            this.CreateScope();
            var service = this.GetService<TService>();
            Context = this.GetService<ExecutionContext>();
            Context.Service = service;
            return service;
        }

        public object GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }
    }

}

