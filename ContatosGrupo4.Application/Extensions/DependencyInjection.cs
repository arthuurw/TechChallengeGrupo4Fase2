using ContatosGrupo4.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContatosGrupo4.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<UsuarioService>();
        services.AddScoped<ContatoService>();
        
        return services;
    }
}