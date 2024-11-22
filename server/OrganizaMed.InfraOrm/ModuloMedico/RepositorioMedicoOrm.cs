using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.Compartilhado;

namespace OrganizaMed.InfraOrm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBaseEmOrm<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public async Task<Medico> SelecionarPorIdAsync(Guid id)
        {
            return await registros.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Medico>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}
