// // using Microsoft.AspNetCore.Identity;
// // using System.Security.Claims;

// // public static class SeedData
// // {
// //     public static async Task EnsureAdminAsync(UserManager<IdentityUser> userManager)
// //     {
// //         string adminEmail = "fade@1234.com";

// //         var user = await userManager.FindByEmailAsync(adminEmail);
// //         if (user == null)
// //         {
// //             user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
// //             await userManager.CreateAsync(user, "Ade1234!"); // your password
// //         }

// //         var claims = await userManager.GetClaimsAsync(user);
// //         if (!claims.Any(c => c.Type == "role" && c.Value == "admin"))
// //         {
// //             await userManager.AddClaimAsync(user, new Claim("role", "admin"));
// //             Console.WriteLine($"✅ {adminEmail} is now an admin");
// //         }
// //     }
// // }

// using Microsoft.AspNetCore.Identity;
// using System.Security.Claims;

// namespace API.Database
// {
//     public static class SeedData
//     {
//         public static async Task EnsureAdminAsync(UserManager<IdentityUser> userManager)
//         {
//             var user = await userManager.FindByEmailAsync("fade@1234.com");
//             if (user != null)
//             {
//                 var claims = await userManager.GetClaimsAsync(user);
//                 if (!claims.Any(c => c.Type == "role" && c.Value == "admin"))
//                 {
//                     await userManager.AddClaimAsync(user, new Claim("role", "admin"));
//                     Console.WriteLine("✅ fade@1234.com is now an admin");
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("⚠️ fade@1234.com not found, cannot assign admin claim");
//             }
//         }
//     }
// }

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Helpers
{
    public static class SeedData
    {
        public static async Task EnsureAdminAsync(UserManager<IdentityUser> userManager)
        {
            var email = "ade@1234.com";
            var password = "Ade1234!";

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new IdentityUser
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    Console.WriteLine("❌ Failed to create admin: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }

                Console.WriteLine($"✅ Created admin account: {email}");
            }

            var claims = await userManager.GetClaimsAsync(user);
            var hasAdmin = claims.Any(c => (c.Type == "role" || c.Type == ClaimTypes.Role) && c.Value == "admin");

            if (!hasAdmin)
            {
                await userManager.AddClaimAsync(user, new Claim("role", "admin"));
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
                Console.WriteLine("✅ Added admin claims to fade@1234.com");
            }
        }
    }
}
