using ContatosGrupo4.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace ContatosGrupo4.Tests.Integration;

public class UsuarioControllerTeste(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetAll_RetornoComSucesso()
    {
        var retorno = await _client.GetAsync("/api/usuarios");
        retorno.EnsureSuccessStatusCode();

        var stringRetorno = await retorno.Content.ReadAsStringAsync();
        var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(stringRetorno);

        Assert.NotEmpty(usuarios);
    }

    [Fact]
    public async Task GetById_RetornoComSucesso_ComUsuario() 
    {
        string login = "teste1";
        var retorno = await _client.GetAsync($"/api/usuarios/{login}");

        retorno.EnsureSuccessStatusCode();
        var stringRetorno = await retorno.Content.ReadAsStringAsync();
        var usuario = JsonConvert.DeserializeObject<Usuario>(stringRetorno);

        Assert.NotNull(usuario);
    }

    [Fact]
    public async Task GetById_RetornoSemSucesso_SemUsuario()
    {
        var login = "teste15";
        var retorno = await _client.GetAsync($"api/usuarios/{login}");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, retorno.StatusCode);
    }

    [Fact]
    public async Task CreateUsuario_DeveRetornarCriado_ComUsuario()
    {
        var usuario = new Usuario { Login = "teste1", Senha = "teste1" };
        var usuarioJson = JsonConvert.SerializeObject(usuario);
        var conteudo = new StringContent(usuarioJson, Encoding.UTF8, "application/json");

        var retorno = await _client.PostAsJsonAsync("/api/usuarios", conteudo);
        retorno.EnsureSuccessStatusCode();
        
        var stringRetorno = await retorno.Content.ReadAsStringAsync();
        var usuarioCriado = JsonConvert.DeserializeObject<Usuario>(stringRetorno);

        Assert.Equal(usuario.Login, usuarioCriado.Login);
    }

    [Fact]
    public async Task CreateUsuario_DeveRetornarBadRequest_UsuarioInvalido()
    {
        var retorno = await _client.PostAsync("/api/usuarios", new StringContent("", Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, retorno.StatusCode); 
    }

    [Fact]
    public async Task UpdateUsuario_DeveRetornarNoContent()
    {
        string login = "teste1";
        var usuario = new Usuario { Login = login, Senha = "teste2" };

        var usuarioJson = JsonConvert.SerializeObject(usuario);
        var conteudo = new StringContent(usuarioJson, Encoding.UTF8, "application/json");

        var retorno = await _client.PutAsync($"api/usuarios/{login}", conteudo);
        retorno.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateUsuario_DeveRetornarBadRequest_UsuarioInvalido()
    {
        string login = "teste15";
        var usuarioInvalido = new Usuario { Login = login, Senha = "teste1" };

        var usuarioJson = JsonConvert.SerializeObject(usuarioInvalido);
        var conteudo = new StringContent(usuarioJson, Encoding.UTF8 , "application/json");
        var retorno = await _client.PutAsync($"api/usuarios/{login}", conteudo);

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, retorno.StatusCode);
    }

    [Fact]
    public async Task DeleteUsuario_DeveRetornarNoContent()
    {
        string login = "teste1";
        var retorno = await _client.DeleteAsync($"api/usuarios/{login}");

        retorno.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteUsuario_DeveRetornarNotFound_UsuarioInvalido()
    {
        string login = "teste15";
        var retorno = await _client.DeleteAsync($"api/usuarios/{login}");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, retorno.StatusCode);
    }
}