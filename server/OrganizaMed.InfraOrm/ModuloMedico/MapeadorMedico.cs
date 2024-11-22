using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Infra.Orm.ModuloMedico
{
    public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.CRM)
                .HasColumnType("varchar(8)")
                .IsRequired();

            builder.Property(x => x.Ocupado)
                .HasColumnType("bit")
                .IsRequired();

            //builder.HasOne(x => x.Usuario)
            //    .WithMany()
            //    .IsRequired()
            //    .HasForeignKey(x => x.UsuarioId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}