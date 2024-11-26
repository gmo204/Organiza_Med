using System.Security.Claims;
using OrganizaMed.Dominio.ModuloAutenticacao;

namespace OrganizaMed.WebApi.Identity
{
    public class ApiTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor ContextAccessor;

        public ApiTenantProvider(IHttpContextAccessor contextAccessor)
        {
            this.ContextAccessor = contextAccessor;
        }
        public Guid? UsuarioId 
        {
            get
            {
                var claimId = ContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

                if (claimId == null)
                    return null;
                

                return Guid.Parse(claimId.Value);
            }
        }
    }
}
