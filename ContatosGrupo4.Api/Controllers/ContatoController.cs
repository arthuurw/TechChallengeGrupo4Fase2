using ContatosGrupo4.Application.Services;
using ContatosGrupo4.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContatosGrupo4.Api.Controllers;

[Route("api/contatos")]
[ApiController]
public class ContatoController (ContatoService contatoService): ControllerBase
{

    private readonly ContatoService _contatoService = contatoService;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _contatoService.GetAllAsync());

    [HttpGet("{login}")] public async Task<IActionResult> GetById(String login) => Ok(await _contatoService.GetByIdAsync(login));

    [HttpPost]
    public async Task<IActionResult> Create(Contato contato)
    {
        await _contatoService.AddAsync(contato);
        return CreatedAtAction(nameof(GetById), new { login = contato.Usuario.Login }, contato);
    }

    [HttpPut("{login}")]
    public async Task<IActionResult> Update(String login, Contato contato)
    {
        if (login != contato.Usuario.Login) return BadRequest();
        await _contatoService.UpdateAsync(contato);
        return NoContent();
    }

    [HttpDelete("{login}")]
    public async Task<IActionResult> Delete(String login)
    {
        await _contatoService.DeleteAsync(login);
        return NoContent();
    }
}