using OrganizaMed.Dominio.ModuloAutenticacao;
using OrganizaMed.Infra.Orm.Compartilhado;
using OrganizaMed.InfraOrm.ModuloAtividade;
using OrganizaMed.InfraOrm.ModuloMedico;

namespace OrganizaMed.Testes.Integracao.Compartilhado
{
    public abstract class RepositorioEmOrmTestsBase
    {
        protected OrganizaMedDbContext dbContext;

        protected RepositorioMedicoOrm repositorioMedico;
        protected RepositorioAtividadeOrm repositorioAtividade;

        protected Usuario usuarioAutenticado;

    }
}
