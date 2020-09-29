using webapi.Entities;
using webapi.Infrastructure.Specifications.Base;

namespace webapi.Infrastructure.Specifications
{
    public class AccountWithCategotyCitySpecification: BaseSpecification<Account>
    {
        public AccountWithCategotyCitySpecification(string identityId)
            : base(x=>x.IdentityId==identityId)
        {
            AddInclude(x=>x.AccountCities);
            AddInclude(x=>x.AccountCategories);
        }
    }
}