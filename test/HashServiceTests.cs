using System.Collections.Generic;
using ServeUp.Models;
using ServeUp.System;
using Xunit;

namespace test
{
    public class HashServiceTests
    {

        [Fact]
        public void CreateHash_Success()
        {
            var service = new PasswordHashService();

            var hash = service.Hash("TestPassword");

            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
        }

        [Fact]
        public void VerifyHash_Success()
        {
            var service = new PasswordHashService();

            var hash = service.Hash("TestPassword");

            service = new PasswordHashService();

            var validated = service.Verify("TestPassword", hash);

            Assert.True(validated);
        }

        [Fact]
        public void EnsureVerifyHash_Success()
        {
            var service = new PasswordHashService();

            var hash = service.Hash("TestPassword");

            service = new PasswordHashService();

            service.EnsureVerified("TestPassword", hash);
        }

        [Fact]
        public void EnsureVerifyHash_WrongPassword()
        {
            var service = new PasswordHashService();

            var hash = service.Hash("TestPassword");

            service = new PasswordHashService();

            Assert.Throws<AuthenticationException>(() => service.EnsureVerified("WrongPassword", hash));
        }

        [Fact]
        public void VerifyHash_WrongPassword()
        {
            var service = new PasswordHashService();

            var hash = service.Hash("TestPassword");

            service = new PasswordHashService();

            var result = service.Verify("WrongPassword", hash);

            Assert.False(result);
        }
    }
}