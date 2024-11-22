using FluentValidation;

namespace OrganizaMed.Dominio.ModuloMedico
{
    public class ValidarMedico : AbstractValidator<Medico>
    {
        public ValidarMedico()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3).WithMessage("O título deve conter no mínimo 3 caracteres")
                .MaximumLength(30).WithMessage("O título deve conter no máximo 30 caracteres");

            RuleFor(x => x.CRM)
                .NotEmpty().WithMessage("O CRM é obrigatório")
                .MinimumLength(8).WithMessage("O CRM deve possuir 8 caracteres")
                .MaximumLength(8).WithMessage("O CRM deve possuir 8 caracteres");
        }
    }
}
