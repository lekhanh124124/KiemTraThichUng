// File: Infrastructure/Identity/IdentitySeeder.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace KiemTraThichUng.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        const string username = "admin";
        const string email = "admin@test.com";
        const string password = "Admin@123";

        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null)
            return;

        var user = new ApplicationUser
        {
            UserName = username,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new Exception("Seed user failed: " +
                string.Join(",", result.Errors.Select(e => e.Description)));
        }
    }
}