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

        public async Task<Medico?> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Atividades).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Medico>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }

        public List<Medico> SelecionarMuitos(List<Guid> idsMedicosSelecionados)
        {
            return registros.Where(m => idsMedicosSelecionados.Contains(m.Id)).ToList();
        }

        public async Task<List<Medico>> Filtrar(Func<Medico, bool> predicate)
        {
            var medicos = await registros.Include(x => x.Atividades).ToListAsync();

            return medicos.Where(predicate).ToList();
        }
    }
}
