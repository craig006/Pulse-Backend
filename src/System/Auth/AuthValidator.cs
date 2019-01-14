using System;
using System.Collections.Generic;
using System.Linq;

namespace ServeUp.System
{
    public class AuthValidator
    {
        private readonly IIdentityProvider _identityProvider;

        public AuthValidator(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public void Validate(Object obj)
        {
            ValidateAttributes(obj);   
        }

        private void ValidateAttributes(Object obj)
        {
            var type = obj.GetType();

            var attributes = type.GetCustomAttributes(typeof(AuthAttribute), false).Select(a => (AuthAttribute)a);

            if(attributes.Count() > 0)
                _identityProvider.EnsureIdentity();

            ValidateClaims(attributes);
        }

        private void ValidateClaims(IEnumerable<AuthAttribute> attributes)
        {
            var claims = attributes.Where(a => a.HasClaim).Select(a => a.Claim);

            foreach (var claim in claims)
            {   
                _identityProvider.EnsureClaim(claim);   
            }
        }
    }
}