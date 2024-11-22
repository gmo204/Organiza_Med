using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.Compartilhado;

namespace NoteKepper.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        var novoMedico = new Medico("Pedro", "12345-SC");

        var optionsBuilder = new DbContextOptionsBuilder<OrganizaMedDbContext>();

        var configuracao = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuracao.GetConnectionString("SqlServer");

        optionsBuilder.UseSqlServer(connectionString);

        var dbContext = new OrganizaMedDbContext(optionsBuilder.Options);

        dbContext.Add(novoMedico);

        dbContext.SaveChanges();

        Console.ReadLine();
    }
}