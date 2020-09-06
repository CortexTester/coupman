using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
    }
}


//Party name, identities
//Identity type(email, phone, duns, ip(login or nonlogin), etc)
//Locations street, city, province, country
//ServiceProvider categorties, description, logo 
//Coupon

