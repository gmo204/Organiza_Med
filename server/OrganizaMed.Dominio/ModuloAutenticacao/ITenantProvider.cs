﻿namespace OrganizaMed.Dominio.ModuloAutenticacao
{
    public interface ITenantProvider
    {
        Guid? UsuarioId { get; }
    }
}