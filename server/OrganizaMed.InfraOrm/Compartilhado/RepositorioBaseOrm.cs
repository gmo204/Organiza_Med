using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Dominio.Compartilhado;

namespace OrganizaMed.Infra.Orm.Compartilhado
{
    public abstract class RepositorioBaseEmOrm<TEntidade> where TEntidade : EntidadeBase
    {
        protected readonly OrganizaMedDbContext dbContext;
        protected DbSet<TEntidade> registros;

        public RepositorioBaseEmOrm(IContextoPersistencia ctx)
        {
            this.dbContext = (OrganizaMedDbContext)ctx;
            this.registros = dbContext.Set<TEntidade>();
        }

        public async Task<bool> InserirAsync(TEntidade registro)
        {
            await registros.AddAsync(registro);

            await dbContext.SaveChangesAsync();

            return true;
        }
        public void Editar(TEntidade entidade)
        {
            registros.Update(entidade);

            dbContext.SaveChanges();
        }

        public void Excluir(TEntidade entidade)
        {
            registros.Remove(entidade);

            dbContext.SaveChanges();
        }

        public virtual TEntidade? SelecionarPorId(Guid id)
        {
            return registros.FirstOrDefault(r => r.Id == id);
        }

        public virtual List<TEntidade> SelecionarTodos()
        {
            return registros
                .ToList();
        }
    }
}