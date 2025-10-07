namespace movieapp;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Database; // Your DbContext namespace

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Apply migrations safely
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration failed: {ex.Message}");
            // Log ex if needed
        }

        // Check if the user already exists
        var userEmail = "fade@1234.com";
        var user = await userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true // avoids email confirmation issues
            };

            var password = "Ade1234!";
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                Console.WriteLine("User creation failed:");
                foreach (var error in result.Errors)
                    Console.WriteLine($" - {error.Description}");
            }
            else
            {
                Console.WriteLine($"User {userEmail} created successfully!");
            }
        }
        else
        {
            Console.WriteLine($"User {userEmail} already exists.");
        }
    }
}
