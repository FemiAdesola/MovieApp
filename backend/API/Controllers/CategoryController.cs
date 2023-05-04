using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/categories")]
    public class CategoryController
    {
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async virtual Task<IActionResult> Create(Category request)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async virtual Task<ActionResult<List<Category>>> GetAll()
        {
            _logger.LogInformation("Getting all the genres");
            return new List<Category>() { new Category() { Id = 1, Name = "Comedy" } };
        }
    }
}