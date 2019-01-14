using System.Threading.Tasks;
using ServeUp.Models;
using ServeUp.Queries;

namespace ServeUp.Blocks
{
    public class SaveUserBlock
    {
        private readonly IUpsertQuery<User> _upsertRepository;
        
        public SaveUserBlock(IUpsertQuery<User> upsertRepository)
        {
            _upsertRepository = upsertRepository;
        }

        public async Task Begin(User user)
        {
            await _upsertRepository.Upsert(user);
        }
    }
}