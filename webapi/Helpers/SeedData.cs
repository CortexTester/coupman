using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using webapi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using webapi.Entities;

namespace webapi.Helpers
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
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            IdentityDataContext identityDataContext = provider.GetRequiredService<IdentityDataContext>();
            UserManager<IdentityUser> userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!context.Accounts.Any())
            {
                await addUser(userManager, context, "admin", "admin", "Administrator");
                await addUser(userManager, context, "business", "business", "Business");
                await addUser(userManager, context, "client", "client", "Client", "tester", "client");
                // IdentityUser user = await userManager.FindByNameAsync(businessUser);
                // if (user == null)
                // {
                //     user = new IdentityUser(businessUser);
                //     IdentityResult result = await userManager.CreateAsync(user, businessPassword);
                //     string token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                //     result = await userManager.ConfirmEmailAsync(user, token);
                //     if (!result.Succeeded)
                //     {
                //         throw new Exception("Cannot create user: " + result.Errors.FirstOrDefault());
                //     }
                // }

                // if (!await userManager.IsInRoleAsync(user, "Business"))
                // {
                //     IdentityResult result
                //     = await userManager.AddToRoleAsync(user, "Business");
                //     if (!result.Succeeded)
                //     {
                //         throw new Exception("Cannot add user to role: " + result.Errors.FirstOrDefault());
                //     }
                // }

                // context.Accounts.AddRange(
                //     new Account
                //     {
                //         Role = Role.Business,
                //         IdentityId = user.Id,
                //         PartyName = user.UserName
                //     }
                // );
                // context.SaveChanges();


                // //client
                // user = await userManager.FindByNameAsync(clientUser);
                // if (user == null)
                // {
                //     user = new IdentityUser(businessUser);
                //     IdentityResult result = await userManager.CreateAsync(user, clientUser);
                //     string token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                //     result = await userManager.ConfirmEmailAsync(user, token);
                //     if (!result.Succeeded)
                //     {
                //         throw new Exception("Cannot create user: " + result.Errors.FirstOrDefault());
                //     }
                // }

                // if (!await userManager.IsInRoleAsync(user, "Client"))
                // {
                //     IdentityResult result
                //     = await userManager.AddToRoleAsync(user, "Client");
                //     if (!result.Succeeded)
                //     {
                //         throw new Exception("Cannot add user to role: " + result.Errors.FirstOrDefault());
                //     }
                // }

                // context.Accounts.AddRange(
                //     new Account
                //     {
                //         Role = Role.Client,
                //         IdentityId = user.Id,
                //         FirstName = "Tester",
                //         LastName = "Client"
                //     }
                // );
                // context.SaveChanges();
            }
        }

        private static async Task addUser(UserManager<IdentityUser> userManager, DataContext context, string userName, string password, string role, string firstName = null, string lastName = null ){
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

                if (!await userManager.IsInRoleAsync(user, role))
                {
                    IdentityResult result = await userManager.AddToRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Cannot add user to role: " + result.Errors.FirstOrDefault());
                    }
                }

                context.Accounts.AddRange(
                    new Account
                    {
                        Role = Role.Business,
                        IdentityId = user.Id,
                        PartyName = user.UserName,
                        FirstName = firstName,
                        LastName = lastName
                    }
                );
                context.SaveChanges();
        }

    }
}