using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController (AppDBContext context) : ControllerBase
    {
         [HttpGet]
         public async Task<ActionResult<List<Category>>> GetCategories()
         {
            return await context.Categories.ToListAsync();

         }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetGategoryDetail(string id)
        {
            var category = await context.Categories.FindAsync(id);

            if (category == null) return NotFound();

            return category;

        }
    }
}
