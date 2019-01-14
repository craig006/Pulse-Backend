using System;

namespace ServeUp.System
{
    public class ConflictException: Exception
    {
        public ConflictException(string message): base(message)
        {
            
        }
        
    }
}