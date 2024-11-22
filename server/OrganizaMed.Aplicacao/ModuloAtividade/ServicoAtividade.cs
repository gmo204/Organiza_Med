using FluentResults;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Aplicacao.Medico
{
    public class ServicoAtividade
    {
        private readonly IRepositorioAtividade repositorioAtividade;
        public ServicoAtividade(IRepositorioAtividade repositorioAtividade)
        {
            this.repositorioAtividade = repositorioAtividade;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            var validador = new ValidarAtividade();

            var resultadoValidacao = await validador.ValidateAsync(atividade);

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
            var validador = new ValidarAtividade();

            var resultadoValidacao = await validador.ValidateAsync(atividade);

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
    }
}
