using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/[controller]")]
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

            return Ok(resultado.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir(InserirMedicoViewModel medicoVm)
        {
            var medico = mapeador.Map<Medico>(medicoVm);

            var resultado = await servicoMedico.InserirAsync(medico);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(medicoVm);
        }
    } 
}