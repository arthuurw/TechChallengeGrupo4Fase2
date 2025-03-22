using ContatosGrupo4.Application.Services;
using ContatosGrupo4.Domain.Entities;
using ContatosGrupo4.Domain.Interfaces;
using Moq;

namespace ContatosGrupo4.Tests.Unit;

public class UsuarioServiceTeste
{
    private readonly Mock<IBaseRepository<Usuario>> _mockUsuarioRepository;
    private readonly UsuarioService _usuarioService;

    public UsuarioServiceTeste()
    {
          _mockUsuarioRepository = new Mock<IBaseRepository<Usuario>>();
        _usuarioService = new UsuarioService(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarUsuarios()
    {
        var usuarios = new List<Usuario>
        {
            new() { Login = "teste1", Senha = "teste1" },
            new() { Login = "teste2", Senha = "teste2" },
        };
        _mockUsuarioRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usuarios);

        var result = await _usuarioService.GetAllAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.Login == "teste1");
        Assert.Contains(result, u => u.Login == "teste2");
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarUsuario_CasoExista()
    {
        var usuario = new Usuario { Login = "teste3", Senha = "teste3" };
        _mockUsuarioRepository.Setup(repo => repo.GetByIdAsync(usuario.Login)).ReturnsAsync(usuario);

        var result = await _usuarioService.GetByIdAsync(usuario.Login);

        Assert.NotNull(result);
        Assert.Equal(usuario.Login, result.Login);
        Assert.Equal(usuario.Senha, result.Senha);
    }

    [Fact]
    public async Task AddAsync_DeveCriarUsuario()
    {
        var usuario = new Usuario { Login = "teste1", Senha = "teste1" };
        _mockUsuarioRepository.Setup(repo => repo.AddAsync(usuario)).Returns(Task.CompletedTask);

        await _usuarioService.AddAsync(usuario);

        _mockUsuarioRepository.Verify(repo => repo.AddAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarUsuario()
    {
        var usuario = new Usuario { Login = "teste1", Senha = "teste1" };
        _mockUsuarioRepository.Setup(repo => repo.UpdateAsync(usuario)).Returns(Task.CompletedTask);

        await _usuarioService.UpdateAsync(usuario);

        _mockUsuarioRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_DeveDeletarUsuario()
    {
        string login = "teste1";
        _mockUsuarioRepository.Setup(repo => repo.DeleteAsync(login)).Returns(Task.CompletedTask);

        await _usuarioService.DeleteAsync(login);

        _mockUsuarioRepository.Verify(repo => repo.DeleteAsync(It.IsAny<string>()), Times.Once);
    }
}