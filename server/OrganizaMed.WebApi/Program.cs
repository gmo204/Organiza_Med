using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.Compartilhado;
using OrganizaMed.InfraOrm.ModuloAtividade;
using OrganizaMed.InfraOrm.ModuloMedico;
using OrganizaMed.WebApi.Config;
using OrganizaMed.WebApi.Config.Mappiing;
using OrganizaMed.WebApi.Config.Mapping;
using OrganizaMed.WebApi.Filters;

namespace OrganizaMed.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string politicaCorsPersonalizada = "_politicaCorsPersonalizada";

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, dbOptions => dbOptions.EnableRetryOnFailure());
            });

            builder.Services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            builder.Services.AddScoped<ServicoMedico>();

            builder.Services.AddScoped<IRepositorioAtividade, RepositorioAtividadeOrm>();
            builder.Services.AddScoped<ServicoAtividade>();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<AtividadeProfile>();
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: politicaCorsPersonalizada, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ResponseWrapperFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //app.UseGlobalExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI();

            {
                using var scope = app.Services.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<IContextoPersistencia>();

                if (dbContext is OrganizaMedDbContext organizaMedDbContext)
                {
                    MigradorBancoDados.AtualizarBancoDados(organizaMedDbContext);
                }
            }

            app.UseHttpsRedirection();

            app.UseCors(politicaCorsPersonalizada);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}