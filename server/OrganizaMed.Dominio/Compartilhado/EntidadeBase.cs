using OrganizaMed.Dominio.ModuloAutenticacao;

namespace OrganizaMed.Dominio.Compartilhado
{
    public class EntidadeBase
    {

        public Guid Id { get; set; }

        public EntidadeBase()
        {
            Id = new Guid();
        }

        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
