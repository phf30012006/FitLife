using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlimentacaoController : ControllerBase
{
    private readonly IRegistroAlimentacaoService _alimentacaoService;

    public AlimentacaoController(IRegistroAlimentacaoService alimentacaoService)
    {
        _alimentacaoService = alimentacaoService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroAlimentacaoDto>> GetById(int id)
    {
        var registro = await _alimentacaoService.GetByIdAsync(id);
        if (registro == null)
            return NotFound();

        return Ok(registro);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<RegistroAlimentacaoDto>>> GetByUsuarioId(int usuarioId)
    {
        var registros = await _alimentacaoService.GetByUsuarioIdAsync(usuarioId);
        return Ok(registros);
    }

    [HttpGet("usuario/{usuarioId}/periodo")]
    public async Task<ActionResult<IEnumerable<RegistroAlimentacaoDto>>> GetByPeriodo(
        int usuarioId,
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim)
    {
        var registros = await _alimentacaoService.GetByUsuarioAndPeriodoAsync(usuarioId, dataInicio, dataFim);
        return Ok(registros);
    }

    [HttpPost]
    public async Task<ActionResult<RegistroAlimentacaoDto>> Create(CreateRegistroAlimentacaoDto dto)
    {
        var registro = await _alimentacaoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = registro.Id }, registro);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _alimentacaoService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
