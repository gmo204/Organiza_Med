using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.Compartilhado;
using OrganizaMed.InfraOrm.ModuloAtividade;
using OrganizaMed.InfraOrm.ModuloMedico;
using OrganizaMed.WebApi.Config.Mappiing;
using OrganizaMed.WebApi.Config.Mapping;
using OrganizaMed.WebApi.Filters;
using Serilog;

namespace OrganizaMed.WebApi
{
    public static class DepedencyInjection
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["SqlServer"];

            services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, dbOptions =>
                {
                    dbOptions.EnableRetryOnFailure();
                });
            });
        }

        public static void ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            services.AddScoped<ServicoMedico>();
            
            services.AddScoped<IRepositorioAtividade, RepositorioAtividadeOrm>();
            services.AddScoped<ServicoAtividade>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services) 
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<AtividadeProfile>();
            });
        }

        public static void ConfigureCors(this IServiceCollection services, string politica)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: politica, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }


        public static void ConfigureSerilog(this IServiceCollection services, ILoggingBuilder logging)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logging.ClearProviders();

            services.AddLogging(builder => builder.AddSerilog(dispose: true));
        }

        public static void ConfigureControllersWithFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ResponseWrapperFilter>();
            });
        }
    }
}
