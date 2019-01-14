using System;
using System.Security.Claims;

namespace ServeUp.System
{
    public class ExecutionContext
    {
        public ClaimsIdentity Identity { get; set; }
        public object Input { get; set; }
        public object Output { get; set; }
        public object Service { get; set; }
        public Exception Exception { get; set; }

        public bool Success { get { return Exception == null; } }
    }

}

