using ContatosGrupo4.Application.Services;
using ContatosGrupo4.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContatosGrupo4.Api.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsuarioController (UsuarioService usuarioService): ControllerBase
{
    private readonly UsuarioService _usuarioService = usuarioService;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _usuarioService.GetAllAsync());

    [HttpGet("{login}")]
    public async Task<IActionResult> GetById(String login)
    {
        try
        {
            return Ok(await _usuarioService.GetByIdAsync(login));
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpPost] public async Task<IActionResult> Create(Usuario usuario) 
    {
        if (usuario == null)
        {
            return BadRequest("Dados inválidos");
        }
        try
        {
            await _usuarioService.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { login = usuario.Login }, usuario);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpPut("{login}")] public async Task<IActionResult> Update(String login, Usuario usuario) 
    { 
        if (usuario == null || login != usuario.Login) return BadRequest();
        try
        {
            await _usuarioService.UpdateAsync(usuario);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpDelete("{login}")] public async Task<IActionResult> Delete(String login)
    {
        try
        {
            await _usuarioService.DeleteAsync(login);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
}