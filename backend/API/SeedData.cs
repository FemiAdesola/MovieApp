// using Microsoft.AspNetCore.Identity;
// using System.Security.Claims;

// public static class SeedData
// {
//     public static async Task EnsureAdminAsync(UserManager<IdentityUser> userManager)
//     {
//         string adminEmail = "fade@1234.com";

//         var user = await userManager.FindByEmailAsync(adminEmail);
//         if (user == null)
//         {
//             user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
//             await userManager.CreateAsync(user, "Ade1234!"); // your password
//         }

//         var claims = await userManager.GetClaimsAsync(user);
//         if (!claims.Any(c => c.Type == "role" && c.Value == "admin"))
//         {
//             await userManager.AddClaimAsync(user, new Claim("role", "admin"));
//             Console.WriteLine($"✅ {adminEmail} is now an admin");
//         }
//     }
// }

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Database
{
    public static class SeedData
    {
        public static async Task EnsureAdminAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("fade@1234.com");
            if (user != null)
            {
                var claims = await userManager.GetClaimsAsync(user);
                if (!claims.Any(c => c.Type == "role" && c.Value == "admin"))
                {
                    await userManager.AddClaimAsync(user, new Claim("role", "admin"));
                    Console.WriteLine("✅ fade@1234.com is now an admin");
                }
            }
            else
            {
                Console.WriteLine("⚠️ fade@1234.com not found, cannot assign admin claim");
            }
        }
    }
}

