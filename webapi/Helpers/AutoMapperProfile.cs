using AutoMapper;
using webapi.Entities;
using webapi.Models;

namespace webapi.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AuthenticateResponse>();  
            CreateMap<RegisterRequest, Account>();
        }
    }
}