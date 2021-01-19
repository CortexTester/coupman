using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Repositories;
using webapi.Models.Coupon;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingController : BaseController
    {
        ICategoryRepository categoryRepositoty;
        ICityRepository cityRepository;
        IMapper mapper;
        public SettingController(ICategoryRepository categoryRepositoty,ICityRepository cityRepository, IMapper mapper) //TODO: remove DataContext that is used by postman test, due to missing account info
        {
            this.categoryRepositoty = categoryRepositoty;
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }
        
        [HttpGet("getCategories")]
        public async Task<ActionResult<IEnumerable<CategoryMessage>>> GetCategories()
        {
            var categories = await categoryRepositoty.GetQueryable().ToListAsync();

            return Ok(mapper.Map<IEnumerable<CategoryMessage>>(categories));
        }

         [HttpGet("getCities")]
        public async Task<ActionResult<IEnumerable<CityMessage>>> GetCities()
        {
            var cities = await cityRepository.GetQueryable().ToListAsync();

            return Ok(mapper.Map<IEnumerable<CityMessage>>(cities));
        }
    }
}