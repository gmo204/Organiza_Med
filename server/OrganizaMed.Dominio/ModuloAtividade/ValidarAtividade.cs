using FluentValidation;

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

            RuleFor(x => x.Medicos)
                .NotNull()
                .WithMessage("A lista de médicos é obrigatória.")
                .Must((atividade, medicos) => ValidarQuantidadeDeMedicos(atividade.Tipo, medicos.Count))
                .WithMessage("Quantidade de médicos inválida para o tipo de atividade.");

            RuleFor(x => x.HoraInicio)
                .MustAsync(async (atividade, horaInicio, cancellation) =>
                    await VerificarConflitoDeHorarios(atividade))
                .WithMessage("A atividade entra em conflito com outra atividade que possui um médico em comum ou não respeita o tempo de descanso.");

        }

        private bool ValidarQuantidadeDeMedicos(TipoAtividadeEnum tipo, int quantidade)
        {
            return tipo switch
            {
                TipoAtividadeEnum.Consulta => quantidade == 1, 
                TipoAtividadeEnum.Cirurgia => quantidade >= 1, 
                _ => false
            };
        }

        private async Task<bool> VerificarConflitoDeHorarios(Atividade novaAtividade)
        {
            var medicoIds = novaAtividade.Medicos.Select(m => m.Id).ToList();

            var atividadesExistentes = await repositorioAtividade
                .SelecionarPorMedicosEPeriodoAsync(medicoIds, novaAtividade.HoraInicio);


            foreach (var atividadeExistente in atividadesExistentes)
            {
                var tempoDescanso = GetTempoDeDescanso(atividadeExistente.Tipo);

                var horaFimComDescanso = atividadeExistente.HoraFim.Add(tempoDescanso);

                if (atividadeExistente.HoraInicio < novaAtividade.HoraFim &&
                    horaFimComDescanso > novaAtividade.HoraInicio)
                {
                    return false;
                }
            }

            return true;
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
