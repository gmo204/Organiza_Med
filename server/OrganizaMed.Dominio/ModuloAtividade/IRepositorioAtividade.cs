using Microsoft.Win32;
using OrganizaMed.Dominio.Compartilhado;

namespace OrganizaMed.Dominio.ModuloAtividade
{
    public interface IRepositorioAtividade : IRepositorioBase<Atividade>
    {
        public Task<IEnumerable<Atividade>> SelecionarPorMedicosEPeriodoAsync(IEnumerable<Guid> medicoIds,DateTime inicio);
    }
}
