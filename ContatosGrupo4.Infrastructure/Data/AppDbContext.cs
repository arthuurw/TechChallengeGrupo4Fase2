using ContatosGrupo4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContatosGrupo4.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Contato> Contatos { get; set; }
}