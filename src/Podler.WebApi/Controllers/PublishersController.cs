using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.WebApi.Repositories;

namespace Podler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersRepository _publishersRepository;

        public PublishersController(IPublishersRepository publishersRepository)
        {
            _publishersRepository = publishersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var publisherDb = await _publishersRepository.GetListAsync();

            return Ok(publisherDb);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisherDb = await _publishersRepository.GetAsync(id);

            if (publisherDb == null)
            {
                return NotFound("Editora não encontrada.");
            }

            return Ok(publisherDb);
        }

        [HttpPost]
        public async Task<IActionResult> PostPublisher([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error verifique os campos e tente novamente mais tarde.");
            }

            var publisherDb = await _publishersRepository.GetPublisherByNameAsync(publisher.Name);

            if (publisherDb != null)
            {
                return BadRequest("Já existe uma editora com esse nome.");
            }

            await _publishersRepository.IncludeAsync(publisher);
            var uri = Url.Action("GetPublisher", new { id = publisher.Id });

            return Created(uri, publisher);
        }

        [HttpPut]
        public async Task<IActionResult> PutPublisher([FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error verifique os campos e tente novamente mais tarde.");
            }

            await _publishersRepository.UpdateAsync(publisher);

            return Ok("Editora adiconada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _publishersRepository.GetAsync(id);

            if (publisher == null)
            {
                return BadRequest("Editora não encontrada.");
            }

            await _publishersRepository.RemoveAsync(publisher);

            return NoContent();
        }
    }
}