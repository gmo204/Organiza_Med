using Microsoft.EntityFrameworkCore;

namespace OrganizaMed.Infra.Orm.Compartilhado
{
    public static class MigradorBancoDados
    {
        public static bool AtualizarBancoDados(DbContext dbContext)
        {
            var qtdMigracoespendentes = dbContext.Database.GetPendingMigrations().Count();

            if (qtdMigracoespendentes == 0) return false;

            dbContext.Database.Migrate();

            return true;
        }
    }
}
