using Blog.Data;
using Blog.Extensions;
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
            try
            {
                var categories = 
                    await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Falha interna do servidor"));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, BlogDataContext context)
        {
            try
            {
                Category? categories = await GetById(id, context);

                if (categories == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

                return Ok(new ResultViewModel<Category>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Erro interno do servidor"));
            }
            
        }

        

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            BlogDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            var category = new Category
            {
                Name = model.Name,
                Slug = model.Slug
            };

            try
            {
                await context.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>($"Falha ao adicionar a categoria\n{e.Message} - {e.InnerException}"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna do servidor"));
            }
            
        }

        [HttpPut("v1/categories/{id:int}")]

        public async Task<IActionResult> PutAsync(
            [FromRoute] int id, 
            [FromBody] EditorCategoryViewModel model,
            BlogDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            Category? category = await GetById(id, context);

            if (category == null)
                return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

            category.Name = model.Name;
            category.Slug = model.Slug;

            try
            {
                context.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>($"Falha ao atualizar a categoria\n{e.Message} - {e.InnerException}"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna do servidor"));
            }

            
        }

        [HttpDelete("v1/categories/{id:int}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id, BlogDataContext context)
        {
            Category? category = await GetById(id, context);
            if (category == null)
                return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));
            try
            {
                context.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>($"Falha ao remover a categoria\n{e.Message} - {e.InnerException}"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("Falha interna do servidor"));
            }
            
        }

        private static async Task<Category?> GetById(int id, BlogDataContext context)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
