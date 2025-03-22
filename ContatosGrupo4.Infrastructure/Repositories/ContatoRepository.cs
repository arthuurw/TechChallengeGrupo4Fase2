using ContatosGrupo4.Domain.Entities;
using ContatosGrupo4.Domain.Interfaces;
using ContatosGrupo4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContatosGrupo4.Infrastructure.Repositories;

public class ContatoRepository (AppDbContext context): IBaseRepository<Contato>
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Contato>> GetAllAsync() => await _context.Contatos.ToListAsync();

    public async Task<Contato> GetByIdAsync(string login) => await _context.Contatos.FindAsync(login);

    public async Task AddAsync(Contato entity)
    {
        _context.Contatos.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contato entity)
    {
        _context.Contatos.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string login)
    {
        var contato = await _context.Contatos.FindAsync(login);
        if (contato != null)
        {
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
        }
    }
}