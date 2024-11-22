using OrganizaMed.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloMedico;

namespace OrganizaMed.Dominio.ModuloAtividade
{
    public class Atividade : EntidadeBase
    {
        public List<Medico> Medicos { get; set; }

        public TipoAtividadeEnum Tipo { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }

        protected Atividade() { }

        public Atividade(TipoAtividadeEnum tipo, DateTime horaInicio, DateTime horaFim)
        {
            Tipo = tipo;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
    }
}
