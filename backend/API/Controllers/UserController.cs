// using API.Database;
// using AutoMapper;
// using Microsoft.AspNetCore.Identity;
// using API.DTOs;
// using Microsoft.AspNetCore.Mvc;
// using API.Services.Interface;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.Extensions.Configuration;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using API.Helper;
// using Microsoft.EntityFrameworkCore;

// namespace API.Controllers
// {
//     public class UserController : BaseApiController
//     {

//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly SignInManager<IdentityUser> _signInManager;
//         private readonly IConfiguration _configuration;
//         private readonly AppDbContext _context;
//         private readonly IMapper _mapper;
//         private readonly IAuthenticationRepo _authenticationRepo;

//         public UserController(
//             UserManager<IdentityUser> userManager,
//             SignInManager<IdentityUser> signInManager,
//             IConfiguration configuration,
//             AppDbContext context,
//             IMapper mapper,
//             IAuthenticationRepo authenticationRepo
//             )
//         {
//             _userManager = userManager;
//             _signInManager = signInManager;
//             _configuration = configuration;
//             _context = context;
//             _mapper = mapper;
//             _authenticationRepo = authenticationRepo;
//         }

//         [HttpGet("listUsers")]
//         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
//         public async Task<ActionResult<List<UserDTO>>> GetListUsers(
//             [FromQuery] PaginationDTO paginationDTO)
//         {
//             var queryable = _context.Users.AsQueryable();
//             await HttpContext.InsertParametersPaginationInHeader(queryable);
//             var users = await queryable
//                 .OrderBy(x => x.Email)
//                 .Paginate(paginationDTO)
//                 .ToListAsync();
//             return _mapper.Map<List<UserDTO>>(users);
//         }

//         [HttpPost("makeAdmin")]
//         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
//         public async Task<ActionResult> MakeAdmin([FromBody] string userId)
//         {
//             var user = await _userManager.FindByIdAsync(userId);
//             await _userManager.AddClaimAsync(user!, new Claim("role", "admin"));
//             return NoContent();
//         }

//         [HttpPost("removeAdmin")]
//         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
//         public async Task<ActionResult> RemoveAdmin([FromBody] string userId)
//         {
//             var user = await _userManager.FindByIdAsync(userId);
//             await _userManager.RemoveClaimAsync(user!, new Claim("role", "admin"));
//             return NoContent();
//         }

//         [HttpPost("create")]
//         public async Task<ActionResult<AuthenticationDTO>> Create(
//             [FromBody] UserCredentialsDTO userCredentialsDTO)
//         {
//             var user = new IdentityUser
//             {
//                 UserName = userCredentialsDTO.Email,
//                 Email = userCredentialsDTO.Email
//             };

//             var result = await _userManager.CreateAsync(user, userCredentialsDTO.Password);

//             if (result.Succeeded)
//             {
//                 return await BuildToken(userCredentialsDTO);
//             }
//             else
//             {
//                 return BadRequest(result.Errors);
//             }
//         }

//         [HttpPost("login")]
//         public async Task<ActionResult<AuthenticationDTO>> Login(
//             [FromBody] UserCredentialsDTO userCredentialsDTO)
//         {
//             var result = await _signInManager.PasswordSignInAsync(userCredentialsDTO.Email,
//                 userCredentialsDTO.Password, isPersistent: false, lockoutOnFailure: false);

//             if (result.Succeeded)
//             {
//                 return await BuildToken(userCredentialsDTO);
//             }
//             else
//             {
//                 return BadRequest("Incorrect Login");
//             }
//         }

//         private async Task<ActionResult> BuildToken(UserCredentialsDTO userCredentialsDTO)
//         {
//             var user = await _authenticationRepo.BuildToken(userCredentialsDTO);
//             if (user is null)
//             {
//                 return BadRequest();
//             }
//             return Ok(user);
//         }
//     }
// }

using API.Database;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using API.Services.Interface;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepo _authenticationRepo;

        public UserController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            IMapper mapper,
            IAuthenticationRepo authenticationRepo
        )
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _authenticationRepo = authenticationRepo;
        }

        // =======================
        // Login
        // =======================
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDTO>> Login([FromBody] UserCredentialsDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("Incorrect Login");

            var validPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!validPassword)
                return BadRequest("Incorrect Login");

            var token = await BuildToken(user);
            return Ok(token);
        }

        // =======================
        // Create (register)
        // =======================
        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationDTO>> Create([FromBody] UserCredentialsDTO dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest($"Username '{dto.Email}' is already taken.");

            var user = new IdentityUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var token = await BuildToken(user);
            return Ok(token);
        }

        // =======================
        // Build JWT Token
        // =======================
        private async Task<AuthenticationDTO> BuildToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new AuthenticationDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                // Email = user.Email
            };
        }

        // =======================
        // Seed the default admin user
        // =======================
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var email = "fade@1234.com";
            var password = "Ade1234!";

            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null) return;

            var user = new IdentityUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, password);
            await userManager.AddClaimAsync(user, new Claim("role", "admin"));
        }
    }
}
