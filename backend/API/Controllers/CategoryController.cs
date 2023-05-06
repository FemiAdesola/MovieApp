using API.Database;
using API.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace API.Controllers
{
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly AppDbContext _context;
        protected readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
             _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<Category>(createCategoryDTO);
            _context.Add(createCategoryDTO);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            _logger.LogInformation("Getting all the genres");
            var category = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(category);
        }
    }
}