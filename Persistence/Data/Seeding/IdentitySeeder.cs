using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Data.Seeding
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. AddRoles
            var roles = new[] { "Admin", "Buyer", "Seller" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Add Admin
            var adminEmail = "fatmaelsayedmousa711@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Fatma",
                    LastName = "Elsayed"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // 3. Another User 
            var userEmail = "kerolosgthabet@gmail.com";
            var normalUser = await userManager.FindByEmailAsync(userEmail);
            if (normalUser == null)
            {
                normalUser = new User
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true,
                    FirstName = "Kerolos",
                    LastName = "Thabet"
                };

                var result = await userManager.CreateAsync(normalUser, "Seller@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "Seller");
                }
            }


        } 
    
    }
}
