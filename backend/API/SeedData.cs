// namespace movieapp;

// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using API.Database; // Your DbContext namespace

// public static class SeedData
// {
//     public static async Task InitializeAsync(IServiceProvider serviceProvider)
//     {
//         using var scope = serviceProvider.CreateScope();
//         var services = scope.ServiceProvider;

//         var context = services.GetRequiredService<AppDbContext>();
//         var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
//         var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//         // Apply migrations safely
//         try
//         {
//             await context.Database.MigrateAsync();
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Migration failed: {ex.Message}");
//             // Log ex if needed
//         }

//         // Check if the user already exists
//         var userEmail = "fade@1234.com";
//         var user = await userManager.FindByEmailAsync(userEmail);

//         if (user == null)
//         {
//             user = new IdentityUser
//             {
//                 UserName = userEmail,
//                 Email = userEmail,
//                 EmailConfirmed = true // avoids email confirmation issues
//             };

//             var password = "Ade1234!";
//             var result = await userManager.CreateAsync(user, password);

//             if (!result.Succeeded)
//             {
//                 Console.WriteLine("User creation failed:");
//                 foreach (var error in result.Errors)
//                     Console.WriteLine($" - {error.Description}");
//             }
//             else
//             {
//                 Console.WriteLine($"User {userEmail} created successfully!");
//             }
//         }
//         else
//         {
//             Console.WriteLine($"User {userEmail} already exists.");
//         }
//     }
// }

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Database
{
    public static class SeedData
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager)
        {
            string email = "fade@1234.com";
            string password = "Ade1234!";

            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // Optional: give default user admin role
                    await userManager.AddClaimAsync(user, new Claim("role", "admin"));
                }
                else
                {
                    throw new Exception($"Seeding default user failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
