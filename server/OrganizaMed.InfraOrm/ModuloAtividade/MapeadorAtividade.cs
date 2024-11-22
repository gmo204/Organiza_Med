using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizaMed.Dominio.ModuloAtividade;

namespace OrganizaMed.InfraOrm.ModuloAtividade
{
    public class MapeadorAtividade : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("TBAtividade");

            builder.Property(a => a.Id)
                .ValueGeneratedNever();

            builder.Property(a => a.Tipo)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .IsRequired();

            builder.HasMany(x => x.Medicos);

            //builder.HasOne(x => x.Usuario)
            //    .WithMany()
            //    .IsRequired()
            //    .HasForeignKey(x => x.UsuarioId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
