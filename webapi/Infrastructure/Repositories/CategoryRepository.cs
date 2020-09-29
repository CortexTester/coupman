using webapi.Entities;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Repositories.Base;

namespace webapi.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository<Category> { }

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }
    }
}