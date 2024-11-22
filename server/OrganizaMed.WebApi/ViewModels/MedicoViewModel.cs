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
    //public class VizualizarMedicoViewModel
    //{
    //    public required Guid Id { get; set; }
    //    public required string Nome { get; set; }
    //    public required string CRM { get; set; }
    //}
}
