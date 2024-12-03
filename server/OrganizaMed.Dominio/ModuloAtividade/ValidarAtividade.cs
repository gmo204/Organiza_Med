using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaMed.Dominio.ModuloAtividade
{
    public class ValidarAtividade : AbstractValidator<Atividade>
    {
        private readonly IRepositorioAtividade repositorioAtividade;

        public ValidarAtividade(IRepositorioAtividade repositorioAtividade)
        {
            this.repositorioAtividade = repositorioAtividade;

            RuleFor(x => x.HoraInicio)
                .NotNull()
                .WithMessage("O horário de início é obrigatório.");

            RuleFor(x => x.HoraFim)
                .NotNull()
                .WithMessage("O horário de término é obrigatório.");

            RuleFor(x => x.HoraFim)
                .GreaterThan(x => x.HoraInicio)
                .WithMessage("O horário de término deve ser maior que o horário de início.");

            RuleFor(x => x)
                .MustAsync(async (novaAtividade, cancellation) =>
                    await VerificarConflitos(novaAtividade))
                .WithMessage("A atividade entra em conflito com outra atividade ou não respeita o tempo de descanso.");
        }

        private async Task<bool> VerificarConflitos(Atividade novaAtividade)
        {
            var medicoIds = novaAtividade.Medicos.Select(m => m.Id);

            var atividadesConflitantes = await repositorioAtividade
                .SelecionarPorMedicosEPeriodoAsync(medicoIds, novaAtividade.HoraInicio, novaAtividade.HoraFim);

            foreach (var atividade in atividadesConflitantes)
            {
                var tempoDescanso = GetTempoDeDescanso(atividade.Tipo);

                if (atividade.HoraFim.Add(tempoDescanso) > novaAtividade.HoraInicio)
                {
                    return false; // Conflito encontrado
                }
            }

            return true; // Sem conflitos
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
