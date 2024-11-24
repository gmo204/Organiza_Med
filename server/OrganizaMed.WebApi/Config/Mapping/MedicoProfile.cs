using AutoMapper;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mappiing
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<Medico, ListarMedicoViewModel>();
            CreateMap<Medico, VisualizarMedicoViewModel>();

            CreateMap<Atividade, VisualizarAtividadeViewModel>();

            CreateMap<InserirMedicoViewModel, Medico>();
            CreateMap<EditarMedicoViewModel, Medico>();
        }
    }
}
