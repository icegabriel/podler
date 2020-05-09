using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComicsController : ControllerBase
    {
        private readonly IComicsRepository _comicsRepository;

        public ComicsController(IComicsRepository comicsRepository)
        {
            _comicsRepository = comicsRepository;
        }

        [HttpGet("{id}/cover")]
        public async Task<IActionResult> GetCover(int id)
        {
            var cover = await _comicsRepository.GetCoverAsync(id);

            if (cover == null)
            {
                return NotFound();
            }

            return File(cover, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComics()
        {
            var comicsDb = await _comicsRepository.GetListAsync();

            return Ok(comicsDb);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComic(int id)
        {
            var comicDb = await _comicsRepository.GetAsync(id);

            if (comicDb == null)
            {
                return NotFound();
            }

            return Ok(comicDb);
        }

        [HttpPost]
        public async Task<IActionResult> PostComic([FromBody] Comic comic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _comicsRepository.IncludeAsync(comic);
            var uri = Url.Action("GetComic", new { id = comic.Id });

            return Created(uri, comic);
        }

        [HttpPut]
        public async Task<IActionResult> PutComic([FromBody] Comic comic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _comicsRepository.UpdateAsync(comic);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            var comic = await _comicsRepository.GetAsync(id);

            if (comic == null)
            {
                return BadRequest();
            }

            await _comicsRepository.RemoveAsync(comic);

            return NoContent();
        }
    }
}
