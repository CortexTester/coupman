using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace webapi.Models
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

            if (!context.Parties.Any())
            {
                context.Parties.AddRange(
                    new Party
                    {
                        Name = "Mega Roffing",
                        Category = "Construction",
                        Description = "Roffing company"
                    }, 
                     new Party
                    {
                        Name = "Oildex Solutions",
                        Category = "Software",
                        Description = "Open Invoice"
                    }
                );
                context.SaveChanges();
            }
        }

    }
}