using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Party> Parties { get; set; }
    }
}


//Party name, identities
//Identity type(email, phone, duns, ip(login or nonlogin), etc)
//Locations street, city, province, country
//ServiceProvider categorties, description, logo 
//Coupon

