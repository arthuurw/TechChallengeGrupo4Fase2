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

    [HttpGet("{login}")] public async Task<IActionResult> GetById(String login) => Ok(await _usuarioService.GetByIdAsync(login));

    [HttpPost] public async Task<IActionResult> Create(Usuario usuario) 
    { 
        await _usuarioService.AddAsync(usuario); 
        return CreatedAtAction(nameof(GetById), new { login = usuario.Login }, usuario); 
    }

    [HttpPut("{login}")] public async Task<IActionResult> Update(String login, Usuario usuario) 
    { 
        if (login != usuario.Login) return BadRequest(); 
        await _usuarioService.UpdateAsync(usuario); 
        return NoContent(); 
    }

    [HttpDelete("{login}")] public async Task<IActionResult> Delete(String login)
    {
        await _usuarioService.DeleteAsync(login); 
        return NoContent();
    }
}