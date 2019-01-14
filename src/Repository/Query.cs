using Microsoft.Azure.Documents.Client;
using ServeUp.Database;
using System.Linq;

namespace ServeUp.Queries
{
    public class Query<T> where T: Document
    {
        private readonly DatabaseConnector _connector;
        
        protected virtual FeedOptions FeedOptions { get => null; }

        public Query(DatabaseConnector databaseConnector)
        {
            _connector = databaseConnector;
        }

        public IQueryable<T> BaseQuery
        {
            get
            {
                return _connector.Client.CreateDocumentQuery<T>(_connector.CollectionUri, FeedOptions).Where(d => d.Type == typeof(T).Name);
            }
        }
    }
}