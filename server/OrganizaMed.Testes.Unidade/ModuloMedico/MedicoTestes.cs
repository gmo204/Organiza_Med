using FluentValidation.TestHelper;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Dominio.Testes.ModuloMedico
{
    [TestClass]
    [TestCategory("Unidade")]
    public class MedicoTestes(IRepositorioMedico repositorioMedico)
    {
        private ValidarMedico validador;
        [TestInitialize]
        public void Inicializar()
        {
            validador = new ValidarMedico(repositorioMedico);
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Valida()
        {
            var medico = new Medico("Pedro", "12345-SC");

            var resultado = validador.TestValidate(medico);

            Assert.IsTrue(resultado.IsValid);
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_Nome_Nulo()
        {
            var medico = new Medico("", "12345-SC");

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(m => m.Nome)
                .WithErrorMessage("O campo nome é obrigatório");
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_Nome_Pequeno()
        {
            var medico = new Medico("Pe", "12345-SC");

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(m => m.Nome)
                .WithErrorMessage("O nome deve conter no mínimo 3 caracteres");
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_Nome_Grande()
        {
            var medico = new Medico("qwertyuiopasdfghjklzxcvbnmqwert", "12345-SC");

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(m => m.Nome)
                .WithErrorMessage("O nome deve conter no máximo 30 caracteres");
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_CRM_Nulo()
        {
            var medico = new Medico("Pedro", "");

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(m => m.CRM)
                .WithErrorMessage("O campo CRM é obrigatório");
        }
        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_CRM_Invalido()
        {
            var medico = new Medico("Pedro", "t3st3#00");

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(m => m.CRM)
                .WithErrorMessage("O CRM deve seguir o padrão 00000-UF");
        }
    }
}