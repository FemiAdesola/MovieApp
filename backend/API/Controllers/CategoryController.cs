using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace API.Controllers
{
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly AppDbContext _context;

        public CategoryController(ILogger<CategoryController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Category request)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            _logger.LogInformation("Getting all the genres");
            return await _context.Categories.ToListAsync();
        }
    }
}