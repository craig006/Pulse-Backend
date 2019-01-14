using System;

namespace ServeUp.System
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base("You are not authorized to perform this action")
        {
            
        }
    }
}