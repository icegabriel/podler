using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.WebApi.Repositories;

namespace Podler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoryRepository;

        public CategoriesController(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categoriesDb = await _categoryRepository.GetListAsync();

            return Ok(categoriesDb);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categoryDb = await _categoryRepository.GetAsync(id);

            if (categoryDb == null)
            {
                return NotFound();
            }

            return Ok(categoryDb);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryDb = await _categoryRepository.GetCategoryByNameAsync(category.Name);

            if (categoryDb != null)
            {
                return Conflict();
            }

            await _categoryRepository.IncludeAsync(category);
            var uri = Url.Action("GetCategory", new { id = category.Id });

            return Created(uri, category);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _categoryRepository.UpdateAsync(category);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category == null)
            {
                return BadRequest();
            }

            await _categoryRepository.RemoveAsync(category);

            return NoContent();
        }
    }
}