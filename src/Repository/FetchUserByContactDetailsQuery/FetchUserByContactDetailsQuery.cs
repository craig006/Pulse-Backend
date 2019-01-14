using System.Threading.Tasks;
using ServeUp.Models;
using System.Linq;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Logging;
using ServeUp.Database;

namespace ServeUp.Queries
{
    public class FetchUserByContactDetailsQuery : Query<User>, IFetchUserByContactDetailsQuery
    {
        private readonly ILogger _logger;

        public FetchUserByContactDetailsQuery(DatabaseConnector databaseConnector, ILogger logger): base(databaseConnector) 
        {
            _logger = logger;//loggerFcatory.CreateLogger<FetchUserByContactDetailsQuery>();
        }

        public async Task<User> Begin(int cellNumber, string email)
        {
            var query = BaseQuery.Where(u => u.CellNumber == cellNumber || u.Email == email);
            var result = await query.AsDocumentQuery().ToListAsync();
            _logger.LogCritical($"FetchUserByContactDetailsQuery charge: ${result.RequestCharge}");
            return result.FirstOrDefault();
        }
    }
}