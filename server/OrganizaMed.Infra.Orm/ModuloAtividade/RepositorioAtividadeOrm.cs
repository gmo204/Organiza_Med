using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Infra.Orm.Compartilhado;
using System.Linq;

namespace OrganizaMed.InfraOrm.ModuloAtividade
{
    public class RepositorioAtividadeOrm : RepositorioBaseEmOrm<Atividade>, IRepositorioAtividade
    {
        public RepositorioAtividadeOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public async Task<Atividade?> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Medicos).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Atividade>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medicos).ToListAsync();
        }

        public async Task<IEnumerable<Atividade>> SelecionarPorMedicosEPeriodoAsync(IEnumerable<Guid> medicoIds, DateTime inicio, DateTime fim)
        {
            return await registros
                .Include(x => x.Medicos)
                .Where(a => a.Medicos.Any(m => medicoIds.Contains(m.Id)) &&
                            (
                                // Verifica sobreposição direta
                                a.HoraFim > inicio &&
                                a.HoraInicio < fim ||

                                // Verifica conflito com o tempo de descanso
                                a.HoraFim.Add(GetTempoDeDescanso(a.Tipo)) > inicio &&
                                a.HoraInicio < fim
                            ))
                .ToListAsync();
        }

        private TimeSpan GetTempoDeDescanso(TipoAtividadeEnum tipo)
        {
            return tipo switch
            {
                TipoAtividadeEnum.Cirurgia => TimeSpan.FromHours(4), // Descanso de 4 horas após cirurgias
                TipoAtividadeEnum.Consulta => TimeSpan.FromMinutes(10), // Descanso de 10 minutos após consultas
                _ => TimeSpan.Zero // Caso padrão
            };
        }

    }
}
