using AutoMapper;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mappiing
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<InserirMedicoViewModel, Medico>();
            CreateMap<EditarMedicoViewModel, Medico>();
            CreateMap<ListarMedicoViewModel, Medico>();
        }
    }
}
