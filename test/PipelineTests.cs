using System;
using ServeUp.Blocks.Security;
using ServeUp.Stacks.Authentication;
using Microsoft.Extensions.DependencyInjection;
using ServeUp.System;
using Xunit;
using ServeUp.Models;
using System.Collections.Generic;
using Tests.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Tests
{
    public class PipelineTests
    {
        public class TestPipeLine : Pipeline 
        {
            public override void ConfigureServices(ServiceCollection services)
            {
                services.AddScoped<ExecutionContext>();
                services.AddScoped<IIdentityProvider, IdentityProvider>();
                
                services.AddTransient<NoAuthRequired>();
                services.AddTransient<AuthAndClaimRequired>();
                services.AddTransient<AuthValidator>();
                services.AddTransient<ExceptionHandlingMiddleware>();
                services.AddTransient<AuthorisationMiddleware>();

                services.AddLogging();
            }

            public override void ConfigureMiddleware(MiddlewarePipeLine collection)
            {
                collection.Use(this.GetService<ExceptionHandlingMiddleware>());
                collection.Use(this.GetService<AuthorisationMiddleware>());
            }

            public override void ConfigureApplication() {}
        }

        [Fact]
        public async void NoAuthRequired_NoUser()
        {
            var pipeline = new TestPipeLine();

            await pipeline.ExecuteStack<NoAuthRequired>();
            
            Assert.Null(pipeline.Context.Exception);
        }

        [Fact]
        public async void AuthRequired_NoUser()
        {
            var pipeline = new TestPipeLine();

            await pipeline.ExecuteStack<AuthAndClaimRequired>();
            
            Assert.IsAssignableFrom(typeof(AuthenticationException), pipeline.Context.Exception);
        }

        [Fact]
        public async void AuthAndClaimRequire_UserNoClaim()
        {
            var pipeline = new TestPipeLine();

            pipeline.GetService<IIdentityProvider>().CurrentIdentity = new Identity();

            await pipeline.ExecuteStack<AuthAndClaimRequired>();
            
            Assert.IsAssignableFrom(typeof(AuthorizationException), pipeline.Context.Exception);
        }
    }
}
