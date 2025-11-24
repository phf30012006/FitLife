using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitosController : ControllerBase
{
    private readonly IHabitoService _habitoService;

    public HabitosController(IHabitoService habitoService)
    {
        _habitoService = habitoService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HabitoDto>> GetById(int id)
    {
        var habito = await _habitoService.GetByIdAsync(id);
        if (habito == null)
            return NotFound();

        return Ok(habito);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<HabitoDto>>> GetByUsuarioId(int usuarioId)
    {
        var habitos = await _habitoService.GetByUsuarioIdAsync(usuarioId);
        return Ok(habitos);
    }

    [HttpPost]
    public async Task<ActionResult<HabitoDto>> Create(CreateHabitoDto dto)
    {
        var habito = await _habitoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = habito.Id }, habito);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _habitoService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
