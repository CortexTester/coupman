using System.Linq;

namespace webapi.Models
{
    public class EFCoupmanRepository : ICoupmanRepository
    {
        private DataContext context;
        public EFCoupmanRepository(DataContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Party> Parties => context.Parties;
    }
}