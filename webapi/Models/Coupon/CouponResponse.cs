using System.Collections.Generic;

namespace webapi.Models.Coupon
{

    public class CouponResponse
    {
        public int CouponId { get; set; }
        public string Title { get; set; }
        public int AccountId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public string OfferType { get; set; }
        public string OfferValue { get; set; }

        public bool IsSomeConditionApply { get; set; } 
        public bool IsNotValidWithOtherPromotion { get; set; }
        public string CustomCondition { get; set; }
        public List<int> CouponCategories { get; set; }
        public List<int> CouponCities { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}