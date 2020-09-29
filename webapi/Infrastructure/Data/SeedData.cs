using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using webapi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using webapi.Entities;
using System.Collections.Generic;

namespace webapi.Infrastructure.Data
{
    public static class SeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "admin";
        private const string businessUser = "business";
        private const string businessPassword = "business";

        private const string clientUser = "client";
        private const string clientPassword = "client";
        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices.CreateScope().ServiceProvider;
            DataContext context = provider.GetRequiredService<DataContext>();
            // context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            IdentityDataContext identityDataContext = provider.GetRequiredService<IdentityDataContext>();
            UserManager<IdentityUser> userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!context.Accounts.Any())
            {
                await addUser(userManager, context, "admin", "admin", Role.Administrator);
                await addUser(userManager, context, "business", "business", Role.Business);
                await addUser(userManager, context, "client", "client", Role.Client, "tester", "client");
            }
        }

        private static async Task addUser(UserManager<IdentityUser> userManager, DataContext context, string userName, string password, Role role, string firstName = null, string lastName = null)
        {
            IdentityUser user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new IdentityUser(userName);
                IdentityResult result = await userManager.CreateAsync(user, password);
                string token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                result = await userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create user: " + result.Errors.FirstOrDefault());
                }
            }

            if (!await userManager.IsInRoleAsync(user, role.ToString()))
            {
                IdentityResult result = await userManager.AddToRoleAsync(user, role.ToString());
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot add user to role: " + result.Errors.FirstOrDefault());
                }
            }

            if (role == Role.Client)
            {
                context.Accounts.AddRange(
                                    new Account
                                    {
                                        Role = Role.Client,
                                        IdentityId = user.Id,
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Name = user.UserName
                                    }
                                );
                context.SaveChanges();
            }

             if (role == Role.Administrator)
            {
                context.Accounts.AddRange(
                                    new Account
                                    {
                                        Role = Role.Administrator,
                                        IdentityId = user.Id,
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Name = user.UserName
                                    }
                                );
                context.SaveChanges();
            }

            if (role == Role.Business)
            {
                await SeedBusinessData.Seed(context, user.Id);
            }

        }

    }

    class SeedBusinessData
    {
        public static async Task Seed(DataContext context, string identityId)
        {
            await SeedCategoriesAsync(context);
            await SeedCitiesAsync(context);
            await SeedAccountAsync(context, identityId);
            await SeedCouponAsync(context);
        }
        private static async Task SeedCategoriesAsync(DataContext context)
        {
            if (context.Categories.Any())
                return;

            foreach (Category catagory in NextCategory)
            {
                context.Categories.Add(catagory);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCitiesAsync(DataContext context)
        {
            if (context.Cities.Any())
                return;

            foreach (City city in NextCity)
            {
                context.Cities.Add(city);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedAccountAsync(DataContext context, string identityId)
        {
            foreach (Account account in NextAccount)
            {
                account.IdentityId = identityId;
                context.Accounts.Add(account);
                var partyCity1 = new AccountCity
                {
                    AccountId = account.AccountId,
                    CityId = 1,
                    Account = account,
                    City = context.Cities.Where(x => x.CityId == 1).FirstOrDefault()
                };
                context.AccountCities.Add(partyCity1);
                var partyCity2 = new AccountCity
                {
                    AccountId = account.AccountId,
                    CityId = 2,
                    Account = account,
                    City = context.Cities.Where(x => x.CityId == 2).FirstOrDefault()
                };
                context.AccountCities.Add(partyCity2);

                var partyCategory1 = new AccountCategory
                {
                    AccountId = account.AccountId,
                    Account = account,
                    Category = context.Categories.Where(x => x.CategoryId == 1).FirstOrDefault(),
                    CategoryId = 1
                };
                context.AccountCategories.Add(partyCategory1);

                var partyCategory2 = new AccountCategory
                {
                    AccountId = account.AccountId,
                    Account = account,
                    Category = context.Categories.Where(x => x.CategoryId == 2).FirstOrDefault(),
                    CategoryId = 2
                };
                context.AccountCategories.Add(partyCategory2);

                var socialLink1 = new SocialLink { SocialName = SocialName.FaceBook, Link = "https://msn.com" };
                account.SocialLinks.Add(socialLink1);
                var socialLink2 = new SocialLink { SocialName = SocialName.Twitter, Link = "https://yahoo.com" };
                account.SocialLinks.Add(socialLink2);

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCouponAsync(DataContext context)
        {
            if (context.Coupons.Any())
                return;
            foreach (Coupon coupon in NextCoupon)
            {
                context.Coupons.Add(coupon);

                coupon.Account = context.Accounts.Where(x => x.AccountId == 1).FirstOrDefault();

                var couponCity1 = new CouponCity
                {
                    Coupon = coupon,
                    CouponId = coupon.CouponId,
                    City = context.Cities.Where(x => x.CityId == 1).FirstOrDefault(),
                    CityId = 1
                };
                context.CouponCities.Add(couponCity1);
                var couponCity2 = new CouponCity
                {
                    Coupon = coupon,
                    CouponId = coupon.CouponId,
                    City = context.Cities.Where(x => x.CityId == 2).FirstOrDefault(),
                    CityId = 2
                };
                context.CouponCities.Add(couponCity2);

                var couponCategory1 = new CouponCategory
                {
                    Coupon = coupon,
                    CouponId = coupon.CouponId,
                    Category = context.Categories.Where(x => x.CategoryId == 1).FirstOrDefault(),
                    CategoryId = 1
                };
                context.CouponCategories.Add(couponCategory1);

                var couponCategory2 = new CouponCategory
                {
                    Coupon = coupon,
                    CouponId = coupon.CouponId,
                    Category = context.Categories.Where(x => x.CategoryId == 2).FirstOrDefault(),
                    CategoryId = 2
                };
                context.CouponCategories.Add(couponCategory2);

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Category> NextCategory
        {
            get
            {
                yield return new Category { Name = "Air Conditioning And Cooling" };
                yield return new Category { Name = "Appliance Repair and Service" };
                yield return new Category { Name = "Asphalt" };
                yield return new Category { Name = "Bathroom Renovation" };
                yield return new Category { Name = "Cabinetry" };
                yield return new Category { Name = "Carpentry and Carpenters" };
                yield return new Category { Name = "Carpet Cleaning" };
                yield return new Category { Name = "Carpet Installation And Repair" };
            }
        }

        private static IEnumerable<City> NextCity
        {
            get
            {
                yield return new City { Name = "Barrie" };
                yield return new City { Name = "Belleville" };
                yield return new City { Name = "Brampton" };
                yield return new City { Name = "Brant" };
                yield return new City { Name = "Brockville" };
                yield return new City { Name = "Burlington" };
            }
        }

        private static IEnumerable<Account> NextAccount
        {
            get
            {
                yield return new Account
                {
                    Name = "T&T Contracting LLC",
                    Description = "Since 2008, T&T Contracting LLC has dedicated ourselves to quality home construction, commercial remodeling, and specialization in emergency repair and restoration. We’ve worked hard to gain our reputation for delivery of high quality work, honest business dealings, and dedication to our clients. As a full service contractor, we offer custom remodels and builds that meet your needs and expectations.",
                    Email = "tester@TNT.com",
                    Phone = "789-123-4567",
                    ContactPerson = "Smith Joes",
                    Address = "789 6 Ave SW",
                    City = "Burlington",
                    Province = "ON",
                    Country = "CA",
                    Website = "klickon.ca",
                    Status = AccountStatus.Pending,
                    Role = Role.Business
                };
            }
        }

        private static IEnumerable<Coupon> NextCoupon
        {
            get
            {
                yield return new Coupon
                {
                    Title = "20% off your next renovation with us",
                    Description = "20% off your next renovation with us on any renovation over $5000",
                    Status = CouponStatus.Pending,
                    OfferType = OfferType.Percentage,
                    OfferValue = "20%",
                    IsSomeConditionApply = true,
                    IsNotValidWithOtherPromotion = true,
                    CustomCondition = "spend over $5000",
                    StartDate = new DateTime().AddDays(5),
                    EndDate = new DateTime().AddDays(35)
                };
            }
        }

    }
}

