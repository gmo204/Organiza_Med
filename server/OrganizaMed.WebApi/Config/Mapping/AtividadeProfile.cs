using AutoMapper;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

public class AtividadeProfile : Profile
{
    public AtividadeProfile()
    {

        CreateMap<Atividade, ListarAtividadeViewModel>();
        CreateMap<Atividade, VisualizarAtividadeViewModel>();

        CreateMap<InserirAtividadeViewModel, Atividade>()
        .AfterMap<FormsAtividadeMappingAction>();

        CreateMap<EditarAtividadeViewModel, Atividade>();
    }

    public class FormsAtividadeMappingAction(IRepositorioMedico repositorioMedico) : IMappingAction<InserirAtividadeViewModel, Atividade>
    {
        public void Process(InserirAtividadeViewModel source, Atividade destination, ResolutionContext context)
        {
            destination.Medicos = repositorioMedico.SelecionarMuitos(source.MedicoId);
        }
    }
}