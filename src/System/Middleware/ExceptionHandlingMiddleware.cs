using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServeUp.System
{
    public class ExceptionHandlingMiddleware : IMiddleWare
    {
        private readonly ExecutionContext _executionContext;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public Func<Task> Next { get; set; }

        public ExceptionHandlingMiddleware(ExecutionContext executionContext, ILoggerFactory loggerFactory)
        {
            _executionContext = executionContext;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke()
        {
            try
            {
                await Next();
            }
            catch(Exception exception)
            {
                _executionContext.Exception = exception;
                _logger.LogError($"Exception: {exception.ToString()} \n Context: {JsonConvert.SerializeObject(_executionContext)}");
            }
        }
    }

}

