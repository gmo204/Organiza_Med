using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizaMed.Dominio.ModuloAtividade;

namespace OrganizaMed.InfraOrm.ModuloAtividade
{
    public class MapeadorAtividadeOrm : IEntityTypeConfiguration<Atividade>
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
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasMany(x => x.Medicos)
                .WithMany(m => m.Atividades)
                .UsingEntity(t => t.ToTable("TBAtividade_TBMedico"));

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
