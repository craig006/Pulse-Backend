using ServeUp.Models;

namespace ServeUp.System
{
    public class IdentityProvider : IIdentityProvider
    {
        public Identity CurrentIdentity { get; set; }

        public void EnsureIdentity() 
        {
            if(CurrentIdentity == null)
            {
                throw new AuthenticationException();
            }
        }

        public void EnsureClaim(Claim claim) 
        {
            EnsureIdentity();

            var exists = CurrentIdentity.Claims?.Contains(claim);

            if(!exists.GetValueOrDefault())
            {
                throw new AuthorizationException();
            }
        }
    }
}