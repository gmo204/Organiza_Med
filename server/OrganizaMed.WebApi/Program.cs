using OrganizaMed.WebApi.Config;
using OrganizaMed.WebApi.Identity;
using Serilog;

namespace OrganizaMed.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string politicaCors = "_minhaPoliticaCors";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureDbContext(builder.Configuration);

            builder.Services.ConfigureCoreServices();

            builder.Services.ConfigureAutoMapper();

            builder.Services.ConfigureSerilog(builder.Logging);

            builder.Services.ConfigureCors(politicaCors);

            builder.Services.ConfigureIdentity();

            //builder.Services.ConfigureJwt(builder.Configuration);

            builder.Services.ConfigureSwaggerAuthorization();

            builder.Services.ConfigureControllersWithFilters();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseGlobalExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI();

            var migracaoConcluida = app.AutoMigrateDatabase();

            if (migracaoConcluida) Log.Information("Migra��o de banco de dados conclu�da");

            else Log.Information("Nenhuma migra��o de banco de dados pendente");

            app.UseHttpsRedirection();

            app.UseCors(politicaCors);

            app.UseAuthorization();

            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Ocorreu um erro que ocasionou no fechamento da aplica��o.", ex);

                return;
            }
        }
    }
}