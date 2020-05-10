using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.WebApi.Repositories;

namespace Podler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsController(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authorsDb = await _authorsRepository.GetListAsync();

            return Ok(authorsDb);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var authorDb = await _authorsRepository.GetAsync(id);

            if (authorDb == null)
            {
                return NotFound();
            }

            return Ok(authorDb);
        }

        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var authorDb = await _authorsRepository.GetAuthorByNameAsync(author.Name);

            if (authorDb != null)
            {
                return Conflict();
            }

            await _authorsRepository.IncludeAsync(author);
            var uri = Url.Action("GetAuthor", new { id = author.Id });

            return Created(uri, author);
        }

        [HttpPut]
        public async Task<IActionResult> PutAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _authorsRepository.UpdateAsync(author);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorsRepository.GetAsync(id);

            if (author == null)
            {
                return BadRequest();
            }

            await _authorsRepository.RemoveAsync(author);

            return NoContent();
        }
    }
}