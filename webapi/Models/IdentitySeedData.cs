using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "MySecret123$";
        private const string adminRole = "Administrator";
        private const string businessRole = "Business";
        private const string clientRole = "Client";
        public static async Task SeedDatabase(IServiceProvider provider)
        {
            IdentityDataContext identityDataContext = provider.GetRequiredService<IdentityDataContext>();
            if (identityDataContext.Database.GetPendingMigrations().Any())
            {
                identityDataContext.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityRole role = await roleManager.FindByNameAsync(adminRole);
            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (role == null)
            {
                role = new IdentityRole(adminRole);
                IdentityResult result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create admin role: "
                    + result.Errors.FirstOrDefault());
                }
                role = new IdentityRole(businessRole);
                result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create business role: "
                    + result.Errors.FirstOrDefault());
                }
                role = new IdentityRole(clientRole);
                result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create client role: "
                    + result.Errors.FirstOrDefault());
                }
            }
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                IdentityResult result
                = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create user: "
                    + result.Errors.FirstOrDefault());
                }
            }

            if (!await userManager.IsInRoleAsync(user, adminRole))
            {
                IdentityResult result
                = await userManager.AddToRoleAsync(user, adminRole);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot add user to role: "
                    + result.Errors.FirstOrDefault());
                }
            }
        }
    }
}