using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Infra.Orm.Compartilhado;

namespace OrganizaMed.InfraOrm.ModuloAtividade
{
    public class RepositorioAtividadeOrm : RepositorioBaseEmOrm<Atividade>, IRepositorioAtividade
    {
        public RepositorioAtividadeOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public async Task<Atividade> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Medicos).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Atividade>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medicos).ToListAsync();
        }
    }
}
