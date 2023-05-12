using API.Database;
using API.DTOs;
using API.Models;
using API.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Controllers
{
    [Route("api/v1/categories")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="IsAdmin")]
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
            _context.Add(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            // _logger.LogInformation("Getting all the genres");
            var queryable = _context.Categories.AsQueryable();
            
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var category = await queryable
                .OrderBy(x =>x.Name)
                .Paginate(paginationDTO)
                .ToListAsync();

            return _mapper.Map<List<CategoryDTO>>(category);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var category = await _context.Categories
                .OrderBy(x =>x.Name)
                .ToListAsync();

            return _mapper.Map<List<CategoryDTO>>(category);
        }

        [HttpGet( "{Id:int}")]
        public async Task<ActionResult<CategoryDTO>>Get(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryDTO>(category);
        }

       [HttpPut("{Id:int}")]
       public async Task<ActionResult> Update(int Id, [FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);

            if (category == null)
            {
                return NotFound();
            }

            category = _mapper.Map(createCategoryDTO, category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

       [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == Id);

            if (!categoryExists)
            {
                return NotFound();
            }

            _context.Remove(new Category() { Id = Id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}