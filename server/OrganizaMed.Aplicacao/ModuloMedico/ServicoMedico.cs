using FluentResults;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Aplicacao.Medico
{
    public class ServicoMedico
    {
        private readonly IRepositorioMedico repositorioMedico;
        public ServicoMedico(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public async Task<Result<Dominio.ModuloMedico.Medico>> InserirAsync(Dominio.ModuloMedico.Medico medico)
        {
            var validador = new ValidarMedico(repositorioMedico);

            var resultadoValidacao = await validador.ValidateAsync(medico);

            if (!resultadoValidacao.IsValid)
            {
                var erros = resultadoValidacao.Errors
                    .Select(failure => failure.ErrorMessage)
                    .ToList();

                return Result.Fail(erros);
            }

            await repositorioMedico.InserirAsync(medico);

            return Result.Ok();
        }

        public async Task<Result<Dominio.ModuloMedico.Medico>> EditarAsync(Dominio.ModuloMedico.Medico medico)
        {
            var validador = new ValidarMedico(repositorioMedico);

            var resultadoValidacao = await validador.ValidateAsync(medico);

            if (!resultadoValidacao.IsValid)
            {
                var erros = resultadoValidacao.Errors
                    .Select(failure => failure.ErrorMessage)
                    .ToList();

                return Result.Fail(erros);
            }

            repositorioMedico.Editar(medico);

            return Result.Ok();
        }

        public async Task<Result<Dominio.ModuloMedico.Medico>> ExcluirAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            repositorioMedico.Excluir(medico);

            return Result.Ok();
        }

        public async Task<Result<List<Dominio.ModuloMedico.Medico>>> SelecionarTodosAsync()
        {
            var medicos = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(medicos);
        }

        public async Task<Result<Dominio.ModuloMedico.Medico>> SelecionarPorIdAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            return Result.Ok(medico);
        }

        public async Task<Result<List<Dominio.ModuloMedico.Medico>>> Filtrar(Func<Dominio.ModuloMedico.Medico, bool> predicate)
        {
            var notas = await repositorioMedico.Filtrar(predicate);
            
            return Result.Ok(notas);
        }

        public Result<Dominio.ModuloMedico.Medico> AlterarStatus(Dominio.ModuloMedico.Medico medico)
        {
            medico.Ocupado = !medico.Ocupado;

            repositorioMedico.Editar(medico);

            return Result.Ok(medico);
        }
    }
}
