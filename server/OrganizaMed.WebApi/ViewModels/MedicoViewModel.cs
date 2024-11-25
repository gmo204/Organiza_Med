namespace OrganizaMed.WebApi.ViewModels
{
    public class InserirMedicoViewModel
    {
        public required string Nome { get; set; }
        public required string CRM { get; set; }
    }

    public class EditarMedicoViewModel : InserirMedicoViewModel {}

    public class ListarMedicoViewModel : InserirMedicoViewModel
    {
        public required Guid Id { get; set; }
        public required bool Ocupado { get; set; }
    }

    public class VisualizarMedicoViewModel : InserirMedicoViewModel 
    {
        public required Guid Id { get; set; }
        public required List<ListarAtividadeViewModel> Atividades { get; set; }
    }
}
