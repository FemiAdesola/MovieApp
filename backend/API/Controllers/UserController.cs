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

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepo _authenticationRepo;

        public UserController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IConfiguration configuration, 
            AppDbContext context, 
            IMapper mapper,
            IAuthenticationRepo authenticationRepo
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _authenticationRepo = authenticationRepo;
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationDTO>> Create(
            [FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            var user = new IdentityUser { 
                UserName = userCredentialsDTO.Email, 
                Email = userCredentialsDTO.Email };

            var result = await _userManager.CreateAsync(user, userCredentialsDTO.Password);
    
            if (result.Succeeded)
            {
                return await BuildToken(userCredentialsDTO);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDTO>> Login(
            [FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentialsDTO.Email,
                userCredentialsDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userCredentialsDTO);
            }
            else
            {
                return BadRequest("Incorrect Login");
            }
        }

        private async Task<ActionResult> BuildToken(UserCredentialsDTO userCredentialsDTO)
        {
            var user = await _authenticationRepo.BuildToken(userCredentialsDTO);
            if (user is null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}