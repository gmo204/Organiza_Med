using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Infra.Orm.Compartilhado;

namespace OrganizaMed.WebApi.Config
{
    public static class DataBaseMigrationConfig
    {
        public static bool AutoMigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<IContextoPersistencia>();

            bool migracaoConcluida = false;

            if (dbContext is OrganizaMedDbContext organizaMedDbContext)
            {
                migracaoConcluida = MigradorBancoDados.AtualizarBancoDados(organizaMedDbContext);
            }

            return migracaoConcluida;
        }

    }
}
