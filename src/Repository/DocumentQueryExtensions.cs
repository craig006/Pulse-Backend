using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;
using ServeUp.Database;

namespace ServeUp.Database
{
    public static class DocumentQueryExtensions
    {
        public static async Task<QueryResult<T>> ToListAsync<T>(this IDocumentQuery<T> documentQuery)
        {
            var result = new QueryResult<T>();

            while(documentQuery.HasMoreResults)
            {
                var batch = await documentQuery.ExecuteNextAsync<T>();
                result.RequestCharge += batch.RequestCharge;
                result.AddRange(batch);
            }

            return result;
        }
    }
}