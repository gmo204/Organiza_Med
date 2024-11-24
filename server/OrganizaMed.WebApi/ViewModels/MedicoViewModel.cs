using OrganizaMed.Dominio.ModuloAtividade;

namespace OrganizaMed.WebApi.ViewModels
{
    public class InserirMedicoViewModel
    {
        public required string Nome { get; set; }
        public required string CRM { get; set; }
    }
    public class EditarMedicoViewModel
    {
        public required string Nome { get; set; }
        public required string CRM { get; set; }
    }
    public class ListarMedicoViewModel
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string CRM { get; set; }
    }
    public class VisualizarMedicoViewModel
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string CRM { get; set; }

        //public required List<ListarAtividadeViewModel> Atividades { get; set; }
    }
}
