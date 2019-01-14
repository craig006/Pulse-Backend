using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using ServeUp.Database;

namespace ServeUp.Queries
{
    public class UpsertQuery<T> : IUpsertQuery<T>
    {
        private readonly DatabaseConnector _connector;
        private readonly ILogger _logger;

        public UpsertQuery(DatabaseConnector databaseConnector, ILoggerFactory loggerFcatory)
        {
            _logger = loggerFcatory.CreateLogger<UpsertQuery<T>>();
            _connector = databaseConnector;
        }

        public async Task Upsert(T document)
        {
            var result = await _connector.Client.UpsertDocumentAsync(_connector.CollectionUri, document);
            _logger.LogInformation($"Upsert query charge: {result.RequestCharge}");
        }
    }
}