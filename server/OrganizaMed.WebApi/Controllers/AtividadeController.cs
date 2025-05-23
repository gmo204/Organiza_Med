﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloAutenticacao;
using OrganizaMed.WebApi.ViewModels;
using Serilog;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/atividades")]
    [ApiController]
    [Authorize]
    public class AtividadeController : ControllerBase
    {

        private readonly ServicoAtividade servicoAtividade;
        private readonly IMapper mapeador;
        private readonly ITenantProvider tenantProvider;

        public AtividadeController(ServicoAtividade servicoAtividade, IMapper mapeador, ITenantProvider tenantProvider)
        {
            this.mapeador = mapeador;
            this.tenantProvider = tenantProvider;
            this.servicoAtividade = servicoAtividade;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await servicoAtividade.SelecionarTodosAsync();

            var resultVm = mapeador.Map<ListarAtividadeViewModel[]>(results.Value);

            Log.Information("Foram seleciados {QuantidadeRegistros} registros", resultVm.Count());

            return Ok(resultVm);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await servicoAtividade.SelecionarPorIdAsync(id);

            var resultVm = mapeador.Map<VisualizarAtividadeViewModel>(result.Value);

            return Ok(resultVm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InserirAtividadeViewModel atividadeVm)
        {
            var atividade = mapeador.Map<Atividade>(atividadeVm);
             
            var usuarioId = tenantProvider.UsuarioId;

            if (usuarioId == null)
                return Unauthorized();

            atividade.UsuarioId = usuarioId.Value;

            var resultado = await servicoAtividade.InserirAsync(atividade);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(atividadeVm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, EditarAtividadeViewModel atividadeVm)
        {
            var atividadeOriginal = await servicoAtividade.SelecionarPorIdAsync(id);

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
