using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Dominio.Testes.ModuloMedico
{
    [TestClass]
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

            var resultado = validador.Validate(medico);

            Assert.IsTrue(resultado.IsValid);
        }

        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_Nome()
        {
            var medico = new Medico("", "12345-SC");

            var validador = new ValidarMedico(repositorioMedico);

            var resultadoValidacao = validador.Validate(medico);
            
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            List<string> errosEsperados =
            [
                "O nome é obrigatório",
            ];

            CollectionAssert.AreEqual(erros, errosEsperados);
            Assert.AreEqual(erros.Count, errosEsperados.Count);
        }
        
        [TestMethod]
        public void Deve_Criar_Instancia_Com_Erro_CRM()
        {
            var medico = new Medico("Pedro", "t3st3#00");

            var validador = new ValidarMedico(repositorioMedico);

            var resultadoValidacao = validador.Validate(medico);
            
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            List<string> errosEsperados =
            [
                "O CRM deve seguir o padrão 00000-SC",
            ];

            CollectionAssert.AreEqual(erros, errosEsperados);
            Assert.AreEqual(erros.Count, errosEsperados.Count);
        }
    }
}