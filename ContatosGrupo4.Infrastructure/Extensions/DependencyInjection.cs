using ContatosGrupo4.Application.Configurations;
using ContatosGrupo4.Domain.Entities;
using ContatosGrupo4.Domain.Interfaces;
using ContatosGrupo4.Infrastructure.Data.Context;
using ContatosGrupo4.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContatosGrupo4.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(c =>
        {
            c.ConnectionString = configuration.GetConnectionString("SqlServer:ConnectionString");
        });

        services.AddDbContext<AppDbContext>(c =>
        {
            c.UseSqlServer(configuration.GetConnectionString("SqlServer:ConnectionString"));
        }, ServiceLifetime.Scoped);

        services.AddScoped<IBaseRepository<Usuario>, UsuarioRepository>();
        services.AddScoped<IBaseRepository<Contato>, ContatoRepository>();

        return services;
    }
}