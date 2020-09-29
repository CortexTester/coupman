using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<AccountCity> AccountCities { get; set; }
        public DbSet<AccountCategory> AccountCategories { get; set; }

        public DbSet<CouponCity> CouponCities { get; set; }
        public DbSet<CouponCategory> CouponCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>()
                .Property(c => c.Status)
                .HasConversion<string>();
            
            modelBuilder.Entity<Account>()
                .Property(c => c.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Coupon>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Coupon>()
                .Property(c => c.OfferType)
                .HasConversion<string>();
            
            modelBuilder.Entity<SocialLink>()
                .Property(c => c.SocialName)
                .HasConversion<string>();

            modelBuilder.Entity<CouponCategory>().HasKey(cc => new { cc.CategoryId, cc.CouponId });
            modelBuilder.Entity<CouponCity>().HasKey(cc => new { cc.CityId, cc.CouponId });

            modelBuilder.Entity<AccountCategory>().HasKey(pc => new { pc.CategoryId, pc.AccountId });
            modelBuilder.Entity<AccountCity>().HasKey(pc => new { pc.CityId, pc.AccountId });


            base.OnModelCreating(modelBuilder);

        }

    }
}


//Party name, identities
//Identity type(email, phone, duns, ip(login or nonlogin), etc)
//Locations street, city, province, country
//ServiceProvider categorties, description, logo 
//Coupon

