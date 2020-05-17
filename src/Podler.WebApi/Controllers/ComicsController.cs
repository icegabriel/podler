using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.WebApi.Repositories;
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
                return NotFound("Quadrinho não encontrado.");
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
                return NotFound("Quadrinho não encontrado.");
            }

            return Ok(comicDb);
        }

        [HttpPost]
        public async Task<IActionResult> PostComic([FromBody] ComicUpload comicUpload)
        {
            if (!ModelState.IsValid)
                return BadRequest("Error verifique os campos e tente novamente.");

            var comic = await _comicsRepository.GetComicByTitleAsync(comicUpload.Title);

            if (comic != null)
                return BadRequest("Já existe um quadrinho com esse titulo.");

            if (comicUpload.Cover.Length > 5242880)
                return BadRequest("A imagem deve ser menor que 5MB.");

            var comicDb = await _comicsRepository.IncludeComicUploadAsync(comicUpload);
            var uri = Url.Action("GetComic", new { id = comicDb.Id });

            return Created(uri, comicDb);
        }

        [HttpPut]
        public async Task<IActionResult> PutComic([FromBody] ComicUpload comicUpload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error verifique os campos e tente novamente.");
            }

            var comic = await _comicsRepository.ToComic(comicUpload);

            await _comicsRepository.UpdateAsync(comic);

            return Ok("Quadrinho alterado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            var comic = await _comicsRepository.GetAsync(id);

            if (comic == null)
            {
                return BadRequest("Quadrinho não encontrado.");
            }

            await _comicsRepository.RemoveAsync(comic);

            return NoContent();
        }
    }
}
