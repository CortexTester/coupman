namespace webapi.Entities
{
    public class CouponCategory
    {
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}