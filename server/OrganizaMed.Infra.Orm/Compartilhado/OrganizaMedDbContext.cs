using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloAutenticacao;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.ModuloMedico;
using OrganizaMed.InfraOrm.ModuloAtividade;

namespace OrganizaMed.Infra.Orm.Compartilhado
{
    public class OrganizaMedDbContext : IdentityDbContext<Usuario, Cargo, Guid>, IContextoPersistencia
    {
        private readonly ITenantProvider? tenantProvider;

        public OrganizaMedDbContext(DbContextOptions<OrganizaMedDbContext> options, ITenantProvider? tenantProvider = null) : base(options)
        {
            this.tenantProvider = tenantProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (tenantProvider != null)
            {
                modelBuilder.Entity<Medico>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
                modelBuilder.Entity<Atividade>().HasQueryFilter(m => m.UsuarioId == tenantProvider.UsuarioId);
            }

            modelBuilder.ApplyConfiguration(new MapeadorMedicoOrm());
            modelBuilder.ApplyConfiguration(new MapeadorAtividadeOrm());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> GravarAsync()
        {
            await SaveChangesAsync();
            return true;
        }
    }
}