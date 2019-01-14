using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using ServeUp.Database;

namespace ServeUp.Queries
{
    public class FetchQuery<T> : IFetchQuery<T> where T: Document
    {
        private readonly DatabaseConnector _connector;
        private readonly ILogger _logger;

        public FetchQuery(DatabaseConnector databaseConnector, ILoggerFactory loggerFcatory)
        {
            _logger = loggerFcatory.CreateLogger<FetchQuery<T>>();
            _connector = databaseConnector;
        }

        public async Task<T> Fetch(string id)
        {
            var result = await _connector.Client.ReadDocumentAsync<T>(_connector.CreateDocumentUri(id));
            _logger.LogInformation($"Fetch query charge: {result.RequestCharge}");
            return result.Document;
        }
    }
}