using System.Collections.Generic;
using ServeUp.Models;
using ServeUp.System;
using Xunit;

namespace test
{
    public class IdentityProviderTests
    {
        [Fact]
        public void EnsureIdentity_ThrowsAuthenicationRequired_NoUser()
        {
            var provider = new IdentityProvider();

            Assert.Throws<AuthenticationException>(() => provider.EnsureIdentity());
        }

        [Fact]
        public void EnsureClaim_ThrowsAuthenicationRequired_NoUser()
        {
            var provider = new IdentityProvider();

            Assert.Throws<AuthenticationException>(() => provider.EnsureClaim(""));
        }

        [Fact]
        public void EnsureIdentity_Success_WithUser()
        {
            var provider = new IdentityProvider();

            provider.CurrentIdentity = new Identity();

            provider.EnsureIdentity();
        }

        [Fact]
        public void EnsureClaim_Success_WithUserWithClaim()
        {
            var provider = new IdentityProvider();

            provider.CurrentIdentity = new Identity {Claims = new List<string> {"TestClaim"} };

            provider.EnsureClaim("TestClaim");
        }

        [Fact]
        public void EnsureClaim_ThrowsOnWrongClaim_WithUserWithClaim()
        {
            var provider = new IdentityProvider();

            provider.CurrentIdentity = new Identity {Claims = new List<string> {"TestCliaim"} };

            Assert.Throws<AuthorizationException>(() => provider.EnsureClaim("IncorrectClaim"));
        }
    }
}