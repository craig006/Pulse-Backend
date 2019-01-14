using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.Repository;
using ServeUp.Stacks;
using ServeUp.System;
using Xunit;

namespace test
{
    public class EnsureUserDoesNotExistBlockTests
    {
        ServiceProvider _serviceProvider;

        public EnsureUserDoesNotExistBlockTests()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<EnsureUserDoesNotExistBlock>();
            collection.AddTransient<IFetchUserByContactDetailsQuery>(c => {
                return Mock.Of<IFetchUserByContactDetailsQuery>(s => 
                s.Begin(1, It.IsAny<string>()) == Task.FromResult((User)null) && 
                s.Begin(2, It.IsAny<string>()) == Task.FromResult(new User()));
            });

            collection.AddLogging();
            _serviceProvider = collection.BuildServiceProvider();
        }
        
        [Fact]
        public async void EnsureUserDoesNotExist_Success()
        {
            var block = _serviceProvider.GetService<EnsureUserDoesNotExistBlock>();

            var user = new User { CellNumber = 1, Email = "" };
            
            await block.Begin(user);
        }

        [Fact]
        public async void EnsureUserDoesNotExist_Failure()
        {
            var block = _serviceProvider.GetService<EnsureUserDoesNotExistBlock>();

            var user = new User { CellNumber = 2, Email = "" };
            
            await Assert.ThrowsAsync<ConflictException>(async () => await block.Begin(user));
        }
    }
}