namespace OrganizaMed.Dominio.Compartilhado
{
    public interface ITenantProvider
    {
        Guid UsuarioId { get; }
    }
}
