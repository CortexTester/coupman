using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace webapi.Helpers
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            DataContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<DataContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            // if (!context.Users.Any())
            // {
            //     context.Users.AddRange(
            //         new User
            //         {
            //             Name = "Mega Roffing",
            //             Category = "Construction",
            //             Description = "Roffing company"
            //         }, 
            //          new User
            //         {
            //             Name = "Oildex Solutions",
            //             Category = "Software",
            //             Description = "Open Invoice"
            //         }
            //     );
            //     context.SaveChanges();
            // }
        }

    }
}