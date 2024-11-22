using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizaMed.Dominio.ModuloAutenticacao
{
    public interface ITenantProvider
    {
        Guid? UsuarioId { get; }
    }
}