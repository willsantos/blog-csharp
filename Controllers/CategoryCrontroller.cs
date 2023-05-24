using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    
    public class CategoryCrontroller : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task <IActionResult> GetAsync(BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("v1/categories/{id:int}")]
        
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, BlogDataContext context)
        {
            Category? categories = await GetById(id, context);

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            BlogDataContext context)
        {
            var category = new Category
            {
                Name = model.Name,
                Slug = model.Slug
            };

            await context.AddAsync(category);
            await context.SaveChangesAsync();
            return Created($"v1/categories/{category.Id}",category);
        }

        [HttpPut("v1/categories/{id:int}")]

        public async Task<IActionResult> PutAsync(
            [FromRoute] int id, 
            [FromBody] EditorCategoryViewModel model,
            BlogDataContext context)
        {
            Category? category = await GetById(id, context);

            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;
            context.Update(category);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("v1/categories/{id:int}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id, BlogDataContext context)
        {
            Category? category = await GetById(id, context);
            if (category == null)
                return NotFound();

            context.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }

        private static async Task<Category?> GetById(int id, BlogDataContext context)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
