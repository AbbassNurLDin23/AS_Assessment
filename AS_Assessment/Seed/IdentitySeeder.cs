using AS_Assessment.Models;
using Microsoft.AspNetCore.Identity;

namespace AS_Assessment.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123"; // ✅ Use strong password rules
            string adminRole = "Admin";

            // Ensure role exists
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Create user if not exists
            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Admin User"
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                    throw new Exception("Failed to create admin user");

                await userManager.AddToRoleAsync(user, adminRole);
            }
        }
    }
}
