using ExpTracker.Dtos.Masters;
using ExpTracker.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories = await categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid id)
        {
            var category = await categoryService.GetByIdAsync(id);
            return category is null ? NotFound("Could not find this category") : Ok(category);
        }
    }
}
