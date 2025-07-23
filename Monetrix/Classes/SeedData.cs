using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Monetrix.Enums;
using Monetrix.Models;

namespace Monetrix.Classes
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Enum.GetNames(typeof(JobPosition)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var users = userManager.Users;

            var adminExists = await users.AnyAsync(u => u.Position == JobPosition.Admin && u.IsActive);
            if (adminExists)
                return;

            var existingAdmin = await userManager.FindByNameAsync("admin");
            if (existingAdmin == null)
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@bank.com",
                    FullName = "Admin User",
                    Position = JobPosition.Admin,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, JobPosition.Admin.ToString());
                }
            }
        }

    }
}
