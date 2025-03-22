using ContatosGrupo4.Domain.Entities;
using ContatosGrupo4.Domain.Interfaces;

namespace ContatosGrupo4.Application.Services;

public class UsuarioService (IBaseRepository<Usuario> usuarioRepository)
{
    private readonly IBaseRepository<Usuario> _usuarioRepository = usuarioRepository;

    public async Task<IEnumerable<Usuario>> GetAllAsync() => await _usuarioRepository.GetAllAsync();

    public async Task<Usuario?> GetByIdAsync(String login) => await _usuarioRepository.GetByIdAsync(login);

    public async Task AddAsync(Usuario usuario) => await _usuarioRepository.AddAsync(usuario);

    public async Task UpdateAsync(Usuario usuario) => await _usuarioRepository.UpdateAsync(usuario);

    public async Task DeleteAsync(String login) => await _usuarioRepository.DeleteAsync(login);
}