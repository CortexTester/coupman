using webapi.Entities;
using webapi.Infrastructure.Specifications.Base;

namespace webapi.Infrastructure.Specifications
{
    public class CouponWithCityCategory : BaseSpecification<Coupon>
    {
        public CouponWithCityCategory(): base(null)
        {
            AddInclude(p => p.CouponCities);
             AddInclude(p => p.CouponCategories);
        }
       
    }
}