using webapi.Entities;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Repositories.Base;

namespace webapi.Infrastructure.Repositories
{
    public interface ICityRepository : IRepository<City> { }
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DataContext context) : base(context) { }
    }
}