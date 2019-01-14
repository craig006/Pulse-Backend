using System;
using ServeUp.Blocks.Security;
using ServeUp.Stacks.Authentication;
using Microsoft.Extensions.DependencyInjection;
using ServeUp.System;
using Xunit;
using ServeUp.Models;
using System.Collections.Generic;
using Tests.Models;

namespace Tests
{
   
    public class AuthenticatorTests
    {
        ServiceProvider _serviceProvider;

        public AuthenticatorTests()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IIdentityProvider, IdentityProvider>();
            collection.AddTransient<AuthValidator>();
            collection.AddLogging();
            _serviceProvider = collection.BuildServiceProvider();
        }

        [Fact]
        public void NoAuthRequired_NoUser()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = null;
                
                var subject = new NoAuthRequired();

                var validator = _serviceProvider.GetService<AuthValidator>();

                validator.Validate(subject);
            }
        }

        [Fact]
        public void NoAuthRequired_WithUserNoClaim()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = new Identity();
                
                var subject = new NoAuthRequired();

                var validator = _serviceProvider.GetService<AuthValidator>();

                validator.Validate(subject);
            }
        }

        [Fact]
        public void NoAuthRequired_WithUserWithClaim()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = new Identity{ Claims = new List<string> { "TestCliam" } };
                
                var subject = new NoAuthRequired();

                var validator = _serviceProvider.GetService<AuthValidator>();

                validator.Validate(subject);
            }
        }

        [Fact]
        public void AuthRequired_NoUser()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = null;

                var subject = new AuthRequiredWithoutClaim();

                var validator = _serviceProvider.GetService<AuthValidator>();

                Assert.Throws<AuthenticationException>(() => validator.Validate(subject));
            }
        }

        [Fact]
        public void AuthRequired_WithUserNoClaim()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = new Identity();
                
                var subject = new AuthRequiredWithoutClaim();

                var validator = _serviceProvider.GetService<AuthValidator>();

                validator.Validate(subject);
            }
        }

        [Fact]
        public void AuthRequired_WithUserWithClaim()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = new Identity{ Claims = new List<string> { "TestClaim" } };
                
                var subject = new AuthRequiredWithoutClaim();

                var validator = _serviceProvider.GetService<AuthValidator>();

                validator.Validate(subject);
            }
        }

        [Fact]
        public void AuthAndClaimRequired_NoUser()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = null;

                var subject = new AuthAndClaimRequired();

                var validator = _serviceProvider.GetService<AuthValidator>();

                Assert.Throws<AuthenticationException>(() => validator.Validate(subject));
            }
        }

        [Fact]
        public void AuthAndClaimRequired_WithUserNoClaim()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = new Identity();
                
                var subject = new AuthAndClaimRequired();

                var validator = _serviceProvider.GetService<AuthValidator>();

                Assert.Throws<AuthorizationException>(() => validator.Validate(subject));
            }
        }

        [Fact]
        public void AuthAndClaimRequired_WithUserWithClaim()
        {
            using(_serviceProvider.CreateScope())
            {
                _serviceProvider.GetService<IIdentityProvider>().CurrentIdentity = new Identity{ Claims = new List<string> { "TestClaim" } };
                
                var subject = new AuthAndClaimRequired();

                var validator = _serviceProvider.GetService<AuthValidator>();

                validator.Validate(subject);
            }
        }
    }
}
