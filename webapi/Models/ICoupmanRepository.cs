using System.Linq;

namespace webapi.Models
{
    public interface ICoupmanRepository
    {
         IQueryable<Party> Parties {get;}
    }
}