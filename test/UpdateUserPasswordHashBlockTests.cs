using Microsoft.Extensions.DependencyInjection;
using ServeUp.Blocks;
using ServeUp.Models;
using ServeUp.Stacks;
using ServeUp.System;
using Xunit;

namespace test
{
    public class UpdateUserPasswordHashBlockTests
    {
        ServiceProvider _serviceProvider;

        public UpdateUserPasswordHashBlockTests()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<IPasswordHashService, PasswordHashService>();
            collection.AddTransient<UpdateUserPasswordHashBlock>();
            collection.AddLogging();
            _serviceProvider = collection.BuildServiceProvider();
        }
        
        [Fact]
        public async void PasswordSetCorrectly_Success()
        {
            var block = _serviceProvider.GetService<UpdateUserPasswordHashBlock>();
            var hashService = _serviceProvider.GetService<IPasswordHashService>();
            
            var user = new User();

            await block.Begin("TestPassword", user);

            Assert.NotNull(user.PasswordHash);

            var verified = hashService.Verify("TestPassword", user.PasswordHash);
            
            Assert.True(verified);
        }
    }
}