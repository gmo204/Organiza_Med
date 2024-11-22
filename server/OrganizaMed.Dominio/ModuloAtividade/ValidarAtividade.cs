using FluentValidation;

namespace OrganizaMed.Dominio.ModuloAtividade
{
    public class ValidarAtividade : AbstractValidator<Atividade>
    {
        public ValidarAtividade()
        {
            RuleFor(x => x.HoraInicio)
                .NotNull()
                .WithMessage("O Horario é obrigatório");

            RuleFor(x => x.HoraInicio)
                .NotNull()
                .WithMessage("O Horario é obrigatório");
        }
    }
}
