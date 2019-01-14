using System.Threading.Tasks;
using ServeUp.System;

namespace Tests.Models
{
    [Auth("TestClaim")]
    public class AuthAndClaimRequired : IBeginnable
    {
        public Task Begin()
        {
            return Task.CompletedTask;
        }
    }
}