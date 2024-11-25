using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Infra.Orm.Compartilhado;

namespace OrganizaMed.WebApi
{
    public static class DepedencyInjection
    {
        public static void ConfigureDbContext(IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["SQL_SERVER_CONNECTION_STRING"];

            services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, dbOptions =>
                {
                    dbOptions.EnableRetryOnFailure();
                });
            });
        }

    }
}
