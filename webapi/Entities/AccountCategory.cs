namespace webapi.Entities
{
    public class AccountCategory
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}