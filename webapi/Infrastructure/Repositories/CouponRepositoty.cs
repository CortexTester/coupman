using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Entities;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Repositories.Base;
using webapi.Infrastructure.Specifications;

namespace webapi.Infrastructure.Repositories
{
    public interface ICouponRepositoty : IRepository<Coupon>
    {
        Task<IEnumerable<Coupon>> GetCouponListAsync();
        Task<IEnumerable<Coupon>> GetCouponByCityCategoryAsync(int cityId, int categoryId);
    }
    public class CouponRepositoty : Repository<Coupon>, ICouponRepositoty
    {
        public CouponRepositoty(DataContext context) : base(context){}

        public async Task<IEnumerable<Coupon>> GetCouponByCityCategoryAsync(int cityId, int categoryId)
        {
            var spec = new CouponCityCategorySpecification(cityId, categoryId);
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Coupon>> GetCouponListAsync()
        {
            var spec = new CouponWithCityCategory();
            return await GetAsync(spec);
        }
    }
}