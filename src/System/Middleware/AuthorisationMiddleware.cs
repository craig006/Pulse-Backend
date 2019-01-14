using System;
using System.Threading.Tasks;

namespace ServeUp.System
{
    public class AuthorisationMiddleware : IMiddleWare
    {
        private readonly AuthValidator _authValidator;

        private readonly ExecutionContext _executionContext;

        public Func<Task> Next { get; set; }

        public AuthorisationMiddleware(AuthValidator authValidator, ExecutionContext executionContext)
        {
            _executionContext = executionContext;
            
            _authValidator = authValidator;     
        }

        public async Task Invoke()
        {
            _authValidator.Validate(_executionContext.Service);

            await Next();
        }
    }

}

