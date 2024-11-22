using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.Compartilhado;
using OrganizaMed.InfraOrm.ModuloMedico;
using OrganizaMed.WebApi.Config.Mappiing;

namespace OrganizaMed.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var conectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(conectionString);
            });

            builder.Services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            builder.Services.AddScoped<ServicoMedico>();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                //Ambiente de teste

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}