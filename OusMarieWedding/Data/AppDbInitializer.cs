
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OusMarieWedding.Data;
using OusMarieWedding.Models;

namespace OusMarieWedding.Data
{
    public class AppDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();


            // Create roles
            //var roleManager = serviceScope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<IdentityRole>>();

            //var roles = new[] { "Admin" };

            //foreach (var item in roles)
            //{
            //    if (!await roleManager.RoleExistsAsync(item))
            //        await roleManager.CreateAsync(new IdentityRole(item));
            //}
            // Create roles


            if (!context.PlusOnes.Any())
            {
                context.AddRange(new List<PlusOne>
                {

                    new PlusOne() {Name="Yes"},

                    new PlusOne() {Name="No" },
                     










                });
                context.SaveChanges();
            }

            if (!context.Testimonials.Any())
            {
                context.AddRange(new List<Testimonial>
                {

                    new Testimonial() { ImageUrl = null , Name = null , testimonial = null },

                  











                });
                context.SaveChanges();
            }



        }
    }
}










