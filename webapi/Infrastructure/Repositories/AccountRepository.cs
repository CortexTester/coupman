using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Entities;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Repositories.Base;
using webapi.Infrastructure.Specifications;

namespace webapi.Infrastructure.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetAccountListAsync();
        Task<IEnumerable<Account>> GetAccountByNameAsync(string accountName);
        Task<Account> GetAccountByIdentityIdAsync(string identityId);
    }
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext context) : base(context){}

        public async Task<Account> GetAccountByIdentityIdAsync(string identityId)
        {
            var spec = new AccountWithCategotyCitySpecification(identityId);
            return (await GetAsync(spec)).FirstOrDefault();
        }

        public Task<IEnumerable<Account>> GetAccountByNameAsync(string accountName)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAccountListAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}