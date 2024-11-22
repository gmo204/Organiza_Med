using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloAutenticacao;

namespace OrganizaMed.Infra.Orm.Compartilhado
{
    public class OrganizaMedDbContext : IdentityDbContext<Usuario, Cargo, Guid>, IContextoPersistencia
    {
        private readonly IContextoPersistencia tenantProvider;
        public OrganizaMedDbContext(IContextoPersistencia tenantProvider, DbContextOptions<OrganizaMedDbContext> options) 
        {
            this.tenantProvider = tenantProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var conectionString = config.GetConnectionString("SqlServer");

            optionsBuilder.UseSqlServer(conectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(OrganizaMedDbContext).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> GravarAsync()
        {
            await SaveChangesAsync();
            return true;
        }
    }
}