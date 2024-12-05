using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Dominio.Testes.ModuloMedico
{
    [TestClass]
    public class AtividadeTestes(IRepositorioAtividade repositorioAtividade)
    {
        [TestMethod]
        public void Deve_Criar_Instancia_Valida()
        {
            var medico = new Medico("Pedro", "12345-SC");

            var atividade = new Atividade(0, DateTime.Now, DateTime.Now);

            var validador = new ValidarAtividade(repositorioAtividade);

            var resultadoValidacao = validador.Validate(atividade);

            Assert.AreEqual(resultadoValidacao.IsValid, true);
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro()
        {
            var atividade = new Atividade(0, DateTime.Today, DateTime.MinValue);

            var validador = new ValidarAtividade(repositorioAtividade);

            var resultadoValidacao = validador.Validate(atividade);

            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            List<string> errosEsperados =
            [
                "O horário de término deve ser maior que o horário de início.",
                "A lista de médicos é obrigatória."
            ];

            Assert.AreEqual(erros, errosEsperados);
        }
    }
}