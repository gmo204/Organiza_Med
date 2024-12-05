using OrganizaMed.Dominio.ModuloAtividade;

namespace OrganizaMed.WebApi.ViewModels
{
    public class InserirAtividadeViewModel
    {
        public required List<Guid> MedicoId { get; set; }
                
        public required TipoAtividadeEnum Tipo { get; set; }
        public required DateTime HoraInicio { get; set; }
        public required DateTime HoraFim { get; set; }
    }
    public class EditarAtividadeViewModel : InserirAtividadeViewModel { }
    public class ListarAtividadeViewModel
    {
        public required Guid id { get; set; }
        public required List<ListarMedicoViewModel> Medicos { get; set; }

        public required TipoAtividadeEnum Tipo { get; set; }
        public required DateTime HoraInicio { get; set; }
        public required DateTime HoraFim { get; set; }
    }

    public class VisualizarAtividadeViewModel
    {
        public required Guid id { get; set; }
        public required List<ListarMedicoViewModel> Medicos { get; set; }

        public required TipoAtividadeEnum Tipo { get; set; }
        public required DateTime HoraInicio { get; set; }
        public required DateTime HoraFim { get; set; }
    }
}
