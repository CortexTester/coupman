using System.Linq;
using AutoMapper;
using webapi.Entities;
using webapi.Models.Auth;
using webapi.Models.Coupon;

namespace webapi.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AuthenticateResponse>();  
            
            CreateMap<RegisterRequest, Account>();
            
            CreateMap<City, CityMessage>();
            
            CreateMap<Category, CategoryMessage>()
            .ForMember(dest=> dest.Image, opt=>opt.MapFrom(src=>src.Image.Url));
            
            CreateMap<Coupon, CouponResponse>()
            .ForMember(dest=>dest.AccountId, opt=>opt.MapFrom(src=>src.Account.AccountId))
            .ForMember(dest=>dest.Status, opt=>opt.MapFrom(src=>src.Status.ToString()))
            .ForMember(dest=>dest.OfferType, opt=>opt.MapFrom(src=>src.OfferType.ToString()))
            .ForMember(dest=>dest.StartDate, opt=>opt.MapFrom(src=>src.StartDate.ToString()))
            .ForMember(dest=>dest.EndDate, opt=>opt.MapFrom(src=>src.EndDate.ToString()))
            .ForMember(dest=>dest.CouponCities, opt=>opt.MapFrom(x=>x.CouponCities.Select(y=>y.City).ToList()))
            .ForMember(dest=>dest.CouponCategories, opt=>opt.MapFrom(x=>x.CouponCategories.Select(y=>y.Category).ToList()));

            
        }
    }
}