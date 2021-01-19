using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;
using webapi.Infrastructure.Data;
using webapi.Infrastructure.Repositories;
using webapi.Models.Coupon;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponController : BaseController
    {
        ICouponRepositoty repositoty;
        IMapper mapper;

        DataContext context;
        public CouponController(ICouponRepositoty repositoty, IMapper mapper, DataContext context) //TODO: remove DataContext that is used by postman test, due to missing account info
        {
            this.repositoty = repositoty;
            this.mapper = mapper;
            this.context = context;
        }

        // [Authorize(Role.Administrator)]
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<CouponResponse>>> GetAll()
        {
            //TODO: should move to service lever
            var coupons = await repositoty.GetQueryable()
                .Include(x => x.CouponCategories).ThenInclude(c => c.Category)
                .Include(x => x.CouponCities).ThenInclude(c => c.City).ToListAsync();

            return Ok(mapper.Map<IEnumerable<CouponResponse>>(coupons));
        }

        [HttpGet("getCouponsByAccountId")]
        public async Task<ActionResult<IEnumerable<CouponResponse>>> getCouponsByAccountId()
        {
            //TODO: should move to service lever
            var coupons = await repositoty.GetQueryable()
                .Where(x => x.EndDate > DateTime.Now)
                .Where(x => x.Status == CouponStatus.Active || x.Status == CouponStatus.Pending)
                .Where(x => x.Account.AccountId == this.Account.AccountId)
                .ToListAsync();


            return Ok(mapper.Map<IEnumerable<CouponResponse>>(coupons));
        }

        [HttpGet("GetCoupon/{id}")]
        public async Task<ActionResult<CouponResponse>> GetCoupon(int id)
        {
            var coupon = await repositoty.GetQueryable()
               .Where(x => x.CouponId == id)
               .Include(x => x.CouponCategories).ThenInclude(c => c.Category)
               .Include(x => x.CouponCities).ThenInclude(c => c.City).FirstOrDefaultAsync();
            if (coupon == null) return NotFound();
            return Ok(mapper.Map<CouponResponse>(coupon));
        }

        [HttpPost("createCoupon")]
        public async Task<ActionResult<CouponResponse>> CreateCoupon(CouponRequest request)
        {
            var coupon = map(request, new Coupon());
            //TODO: move to service level
            coupon = await repositoty.AddAsync(coupon);
            var mappedCoupon = mapper.Map<CouponResponse>(coupon);
            return CreatedAtAction(nameof(GetCoupon), new { id = coupon.CouponId }, mappedCoupon);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CouponResponse>> UpdateCoupon(int id, CouponRequest request)
        {
            var coupon = await repositoty.GetQueryable()
               .Where(x => x.CouponId == id)
               .Include(x => x.CouponCategories).ThenInclude(c => c.Category)
               .Include(x => x.CouponCities).ThenInclude(c => c.City).FirstOrDefaultAsync();

            if (coupon == null) return NotFound();

            coupon = map(request, coupon);
            await repositoty.UpdateAsync(coupon);
            return Ok(mapper.Map<CouponResponse>(coupon));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CouponResponse>> DeleteCoupon(int id)
        {
            var coupon = await repositoty.GetByIdAsync(id);
            if (coupon == null) return NotFound();

            coupon.Status = CouponStatus.Deleted;
            await repositoty.UpdateAsync(coupon);

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CouponResponse>>> GetCouponsByCityCatogory([FromQuery]int cityId, [FromQuery]int categoryId)
        {
            var coupons = await repositoty.GetQueryable()
            .Where(x => x.CouponCities.Any(p => p.CityId == cityId))
            .Where(x => x.CouponCategories.Any(t => t.CategoryId == categoryId)).ToListAsync();
            return Ok(mapper.Map<IEnumerable<CouponResponse>>(coupons));
        }

        private Coupon map(CouponRequest request, Coupon coupon)
        {
            //TODO: should move to mapper
            coupon.Account = this.Account; //this.context.Accounts.First(x => x.AccountId == 1); 
            coupon.Title = request.Title;
            coupon.Status = CouponStatus.Pending;
            coupon.Description = request.Description;
            coupon.OfferType = (OfferType)Enum.Parse(typeof(OfferType), request.OfferType);
            coupon.OfferValue = request.OfferValue;
            coupon.IsSomeConditionApply = request.IsSomeConditionApply;
            coupon.IsNotValidWithOtherPromotion = request.IsNotValidWithOtherPromotion;
            coupon.IsNotValidWithOtherPromotion = request.IsNotValidWithOtherPromotion;
            coupon.CustomCondition = request.CustomCondition;
            coupon.StartDate = DateTime.Parse(request.StartDate);
            coupon.EndDate = DateTime.Parse(request.EndDate);

            //for update coupon method -- find the better way for update many-to-many
            coupon.CouponCategories.Clear();
            coupon.CouponCities.Clear();

            request.CouponCategories.ForEach(x => coupon.CouponCategories.Add(new CouponCategory() { Coupon = coupon, CategoryId = x }));
            request.CouponCities.ForEach(x => coupon.CouponCities.Add(new CouponCity() { Coupon = coupon, CityId = x }));
            return coupon;
        }
    }
}