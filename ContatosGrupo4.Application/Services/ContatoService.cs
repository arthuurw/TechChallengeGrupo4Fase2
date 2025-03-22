using ContatosGrupo4.Domain.Entities;
using ContatosGrupo4.Domain.Interfaces;

namespace ContatosGrupo4.Application.Services;

public class ContatoService (IBaseRepository<Contato> contatoRepository)
{

    private readonly IBaseRepository<Contato> _contatoRepository = contatoRepository;

    public async Task<IEnumerable<Contato>> GetAllAsync() => await _contatoRepository.GetAllAsync();

    public async Task<Contato?> GetByIdAsync(String login) => await _contatoRepository.GetByIdAsync(login);

    public async Task AddAsync(Contato contato) => await _contatoRepository.AddAsync(contato);

    public async Task UpdateAsync(Contato contato) => await _contatoRepository.UpdateAsync(contato);

    public async Task DeleteAsync(String login) => await _contatoRepository.DeleteAsync(login);
}