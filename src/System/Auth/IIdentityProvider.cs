using ServeUp.Models;

namespace ServeUp.System
{
    public interface IIdentityProvider
    {
         Identity CurrentIdentity { get; set; }

         void EnsureIdentity();

         void EnsureClaim(Claim claim);
    }
}