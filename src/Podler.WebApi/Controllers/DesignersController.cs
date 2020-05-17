using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.WebApi.Repositories;

namespace Podler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignersController : ControllerBase
    {
        private readonly IDesignersRepository _designersRepository;

        public DesignersController(IDesignersRepository designersRepository)
        {
            _designersRepository = designersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDesigners()
        {
            var designersDb = await _designersRepository.GetListAsync();

            return Ok(designersDb);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesigner(int id)
        {
            var designer = await _designersRepository.GetAsync(id);

            if (designer == null)
            {
                return NotFound("Desenhista não encontrado.");
            }

            return Ok(designer);
        }

        [HttpPost]
        public async Task<IActionResult> PostDesigner([FromBody] Designer designer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error verifique os campos e tente novamente mais tarde.");
            }

            var designerDb = await _designersRepository.GetDesignerByNameAsync(designer.Name);

            if (designerDb != null)
            {
                return BadRequest("Já existe um desenhista com esse nome.");
            }

            await _designersRepository.IncludeAsync(designer);
            var uri = Url.Action("GetDesigner", new { id = designer.Id });

            return Created(uri, designer);
        }

        [HttpPut]
        public async Task<IActionResult> PutDesigner([FromBody] Designer designer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error verifique os campos e tente novamente mais tarde.");
            }

            await _designersRepository.UpdateAsync(designer);

            return Ok("Desnhista adicionado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesigner(int id)
        {
            var designer = await _designersRepository.GetAsync(id);

            if (designer == null)
            {
                return BadRequest("Desenhista não encontrado.");
            }

            await _designersRepository.RemoveAsync(designer);

            return NoContent();
        }
    }
}