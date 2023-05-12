using System.Net;
using API.Database;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class RatingController : BaseApiController
    {
       private readonly AppDbContext _context;
       private readonly UserManager<IdentityUser> _userManager;

        public RatingController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingDTO ratingDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x=>x.Type =="email")!.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user!.Id;

            var currentRate = await _context.Ratings
            .FirstOrDefaultAsync(x => x.MovieId == ratingDTO.MovieId &&
                x.UserId == userId);

            if (currentRate == null)
            {
                var rating = new Rating();
                rating.MovieId = ratingDTO.MovieId;
                rating.Rate = ratingDTO.Rating;
                rating.UserId = userId;
                _context.Add(rating);
            }
            else
            {
                currentRate.Rate = ratingDTO.Rating;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}