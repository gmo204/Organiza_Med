using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly ServicoMedico servicoMedico;
        private readonly IMapper mapeador;

        public MedicoController(ServicoMedico servico, IMapper mepeador)
        {
            this.servicoMedico = servico;
            this.mapeador = mepeador;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await servicoMedico.SelecionarTodosAsync();

            if(resultado.IsFailed)
                return StatusCode(500);

            var medicoVm = mapeador.Map<ListarMedicoViewModel[]>(resultado.Value);

            return Ok(medicoVm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var medicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            var medicoVm = mapeador.Map<VisualizarMedicoViewModel>(medicoResult.Value);

            return Ok(medicoVm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InserirMedicoViewModel medicoVm)
        {
            var medico = mapeador.Map<Medico>(medicoVm);

            var resultado = await servicoMedico.InserirAsync(medico);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(medicoVm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, EditarMedicoViewModel medicoVm)
        {
            var selecionarOriginal = await servicoMedico.SelecionarPorIdAsync(id);

            if (selecionarOriginal.IsFailed)
                return NotFound(selecionarOriginal.Errors);

            var medicoEditado = mapeador.Map(medicoVm, selecionarOriginal.Value);

            var edicao = await servicoMedico.EditarAsync(medicoEditado);
            
            if (edicao.IsFailed)
                return BadRequest(edicao.Errors);

            return Ok(edicao.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Result = await servicoMedico.ExcluirAsync(id);

            if (Result.IsFailed)
                return NotFound(Result.Errors);

            return Ok();
        }
    }
}