using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using ServeUp.Database;

namespace ServeUp.Queries
{
    public class DeleteQuery : IDeleteQuery
    {
        private readonly DatabaseConnector _connector;
        private readonly ILogger _logger;
        
        public DeleteQuery(DatabaseConnector databaseConnector, ILoggerFactory loggerFcatory)
        { 
            _logger = loggerFcatory.CreateLogger<DeleteQuery>();
            _connector = databaseConnector;
        }

        public async Task Delete(string id)
        {
            var result = await _connector.Client.DeleteDocumentAsync(_connector.CreateDocumentUri(id));
            _logger.LogInformation($"Delete query charge: ${result.RequestCharge}");
        }
    }
}