using FluentResults;
using OrganizaMed.Dominio.ModuloAtividade;

namespace OrganizaMed.Aplicacao.Medico
{
    public class ServicoAtividade
    {
        private readonly IRepositorioAtividade repositorioAtividade;
        private readonly ValidarAtividade validadorAtividade;
        public ServicoAtividade(IRepositorioAtividade repositorioAtividade)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.validadorAtividade = new ValidarAtividade(repositorioAtividade);

        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            var resultadoValidacao = await validadorAtividade.ValidateAsync(atividade);

            if (!resultadoValidacao.IsValid)
            {
                var erros = resultadoValidacao.Errors
                    .Select(failure => failure.ErrorMessage)
                    .ToList();

                return Result.Fail(erros);
            }

            await repositorioAtividade.InserirAsync(atividade);

            return Result.Ok();
        }

        public async Task<Result<Atividade>> EditarAsync(Atividade atividade)
        {
            var resultadoValidacao = await validadorAtividade.ValidateAsync(atividade);

            if (!resultadoValidacao.IsValid)
            {
                var erros = resultadoValidacao.Errors
                    .Select(failure => failure.ErrorMessage)
                    .ToList();

                return Result.Fail(erros);
            }

            repositorioAtividade.Editar(atividade);

            return Result.Ok();
        }

        public async Task<Result<Atividade>> ExcluirAsync(Guid id)
        {
            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            repositorioAtividade.Excluir(atividade);

            return Result.Ok();
        }

        public async Task<Result<List<Atividade>>> SelecionarTodosAsync()
        {
            var atividades = await repositorioAtividade.SelecionarTodosAsync();

            return Result.Ok(atividades);
        }

        public async Task<Result<Atividade>> SelecionarPorIdAsync(Guid id)
        {
            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            return Result.Ok(atividade);
        }

        public async Task<bool> VerificarConflitosAsync(Atividade novaAtividade)
        {
            var medicoIds = novaAtividade.Medicos.Select(m => m.Id).ToList();
            var atividadesExistentes = await repositorioAtividade.SelecionarPorMedicosEPeriodoAsync(
                medicoIds,
                novaAtividade.HoraInicio,
                novaAtividade.HoraFim
            );

            return atividadesExistentes.Any(a =>
                a.HoraFim.Add(GetTempoDeDescanso(a.Tipo)) > novaAtividade.HoraInicio);
        }

        private TimeSpan GetTempoDeDescanso(TipoAtividadeEnum tipo)
        {
            return tipo switch
            {
                TipoAtividadeEnum.Cirurgia => TimeSpan.FromHours(4),
                TipoAtividadeEnum.Consulta => TimeSpan.FromMinutes(10),
                _ => TimeSpan.Zero
            };
        }
    }
}
