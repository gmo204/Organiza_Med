using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/atividades")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private readonly IMapper mapeador;
        private readonly ServicoAtividade servicoAtividade;
        public AtividadeController(ServicoAtividade servicoAtividade, IMapper mapeador)
        {
            this.servicoAtividade = servicoAtividade;
            this.mapeador = mapeador;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await servicoAtividade.SelecionarTodosAsync();

            if (results.IsFailed)
                return StatusCode(500);

            var resultVm = mapeador.Map<ListarAtividadeViewModel[]>(results.Value);

            return Ok(resultVm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await servicoAtividade.SelecionarPorIdAsync(id);

            if (result.IsFailed)
                return NotFound(result.Errors);

            var resultVm = mapeador.Map<VisualizarAtividadeViewModel>(result.Value);

            return Ok(resultVm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InserirAtividadeViewModel atividadeVm)
        {
            var atividade = mapeador.Map<Atividade>(atividadeVm);

            var resultado = await servicoAtividade.InserirAsync(atividade);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(atividadeVm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, EditarAtividadeViewModel atividadeVm)
        {
            var atividadeOriginal = await servicoAtividade.SelecionarPorIdAsync(id);

            if (atividadeOriginal.IsFailed)
                return NotFound(atividadeOriginal.Errors);

            var atividadeEditada = mapeador.Map(atividadeVm, atividadeOriginal.Value);

            var edicao = await servicoAtividade.EditarAsync(atividadeEditada);

            if (edicao.IsFailed)
                return BadRequest(edicao.Errors);

            return Ok(edicao.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await servicoAtividade.ExcluirAsync(id);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok();
        }
    }
}
