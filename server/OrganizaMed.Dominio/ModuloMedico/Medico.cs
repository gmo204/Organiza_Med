using OrganizaMed.Dominio.Compartilhado;
using OrganizaMed.Dominio.ModuloAtividade;

namespace OrganizaMed.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase
    {
        public string Nome { get; set; }
        public string CRM { get; set; }
        public bool Ocupado { get; set; }

        public List<Atividade> Atividades { get; set; }
        
        protected Medico() { }

        public Medico(string nome, string crm)
        {
            Nome = nome;
            CRM = crm;
        }
    }
}
