// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using API.DTOs;
// using API.Services.Interface;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.IdentityModel.Tokens;

// namespace API.Services.Implementations
// {
//     public class AuthenticationRepo : IAuthenticationRepo
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly IConfiguration _configuration;

//         public AuthenticationRepo(
//             UserManager<IdentityUser> userManager,
//             IConfiguration configuration)
//         {
//             _userManager = userManager;
//             _configuration = configuration;
//         }

//         public async Task<AuthenticationDTO> BuildToken(UserCredentialsDTO userCredentialsDTO)
//         {
//             var claims = new List<Claim>()
//             {
//                 new Claim("email", userCredentialsDTO.Email)
//             };

//             var user = await _userManager.FindByNameAsync(userCredentialsDTO.Email);
//             var claimsDB = await _userManager.GetClaimsAsync(user);

//             claims.AddRange(claimsDB);

//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Secret").Value!));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var expiration = DateTime.UtcNow.AddYears(1);

//             var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
//                 expires: expiration, signingCredentials: creds);

//             return new AuthenticationDTO()
//             {
//                 Token = new JwtSecurityTokenHandler().WriteToken(token),
//                 Expiration = expiration
//             };
//         }
//     }
// }

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.DTOs;
using API.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Implementations
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationRepo(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // ✅ Add the BuildToken method here
        public async Task<AuthenticationDTO> BuildToken(UserCredentialsDTO userCredentialsDTO)
        {
            var user = await _userManager.FindByNameAsync(userCredentialsDTO.Email);
            if (user == null)
                throw new Exception("User not found");

            var claims = new List<Claim>
            {
                new Claim("email", user.Email!)
            };

            var claimsDB = await _userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["AppSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret missing")
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new AuthenticationDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Email = user.Email
            };
        }
    }
}
