using FluentValidation;

namespace OrganizaMed.Dominio.ModuloMedico
{
    public class ValidarMedico : AbstractValidator<Medico>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public ValidarMedico(IRepositorioMedico repositorioMedico)
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3).WithMessage("O título deve conter no mínimo 3 caracteres")
                .MaximumLength(30).WithMessage("O título deve conter no máximo 30 caracteres")
                .MustAsync(NomeUnico).WithMessage("Já existe um médico com este nome.");


            RuleFor(x => x.CRM)
                .NotEmpty().WithMessage("O CRM é obrigatório")
                .Matches(@"^\d{5}-[A-Z]{2}$")
                .WithMessage("O CRM deve seguir o padrão 00000-SC");
        }

        private async Task<bool> NomeUnico(string nome, CancellationToken cancellationToken)
        {
            return !await repositorioMedico.ExisteNomeAsync(nome, cancellationToken);
        }
    }
}
