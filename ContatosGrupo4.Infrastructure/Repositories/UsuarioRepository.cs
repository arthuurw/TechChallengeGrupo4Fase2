using ContatosGrupo4.Domain.Entities;
using ContatosGrupo4.Domain.Interfaces;
using ContatosGrupo4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContatosGrupo4.Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext context) : IBaseRepository<Usuario>
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Usuario>> GetAllAsync() => await _context.Usuarios.ToListAsync();

    public async Task<Usuario> GetByIdAsync(string login) => await _context.Usuarios.FindAsync(login);

    public async Task AddAsync(Usuario entity)
    {
        _context.Usuarios.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario entity)
    {
        _context.Usuarios.Update(entity); 
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string login)
    {
        var usuario = await _context.Usuarios.FindAsync(login);
        if (usuario != null) 
        { 
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync(); 
        }
    }
}