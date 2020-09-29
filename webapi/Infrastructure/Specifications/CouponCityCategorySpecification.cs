using System;
using System.Linq;
using System.Linq.Expressions;
using webapi.Entities;
using webapi.Infrastructure.Specifications.Base;

namespace webapi.Infrastructure.Specifications
{
    public class CouponCityCategorySpecification : BaseSpecification<Coupon>
    {
        public CouponCityCategorySpecification(int cityId, int categoryId) 
        : base(x=>x.CouponCities.Any(predicate=>predicate.CityId == cityId) 
            && x.CouponCategories.Any(predicate=>predicate.CategoryId == categoryId))
        {
        }
    }
}