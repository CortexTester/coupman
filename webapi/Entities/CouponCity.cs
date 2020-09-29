namespace webapi.Entities
{
    public class CouponCity
    {
         public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}