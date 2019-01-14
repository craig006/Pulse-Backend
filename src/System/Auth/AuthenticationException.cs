using System;

namespace ServeUp.System
{
    public class AuthenticationException: Exception
    {
        public AuthenticationException() : base("You need to be authenticated to perform this action.")
        {
            
        }
    }
}