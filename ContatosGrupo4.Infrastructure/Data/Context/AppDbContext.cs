using ContatosGrupo4.Application.Configurations;
using ContatosGrupo4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ContatosGrupo4.Infrastructure.Data.Context;

public class AppDbContext(IOptions<DatabaseSettings> databaseSettings) : DbContext
{
    private readonly IOptions<DatabaseSettings> _databaseSettings = databaseSettings;

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Contato> Contatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationManager();
            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            string? connectionString = configuration["SqlServer:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A ConnectionString não foi encontrada no arquivo de configuração.");
            }
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ContatosGrupo4.Api");
        if (!Directory.Exists(basePath))
        {
            throw new DirectoryNotFoundException($"O diretório base '{basePath}' não foi encontrado.");
        }
        Directory.SetCurrentDirectory(basePath);

        var configuration = new ConfigurationManager();
        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        string? connectionString = configuration["SqlServer:ConnectionString"];
        if(string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A ConnectionString não foi encontrada no arquivo de configuração.");
        }

        var databaseSettings = new DatabaseSettings { ConnectionString = connectionString };
        var options = Options.Create(databaseSettings);
        
        return new AppDbContext(options);
    }
}