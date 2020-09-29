namespace webapi.Entities
{
    public class AccountCity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}