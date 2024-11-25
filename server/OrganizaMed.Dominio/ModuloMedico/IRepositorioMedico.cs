using OrganizaMed.Dominio.Compartilhado;

namespace OrganizaMed.Dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorioBase<Medico>
    {
        Task<List<Medico>> Filtrar(Func<Medico, bool> predicate);

    }
}
