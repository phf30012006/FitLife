using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreinosController : ControllerBase
{
    private readonly ITreinoService _treinoService;

    public TreinosController(ITreinoService treinoService)
    {
        _treinoService = treinoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoDto>>> GetAll()
    {
        var treinos = await _treinoService.GetAllAsync();
        return Ok(treinos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TreinoDto>> GetById(int id)
    {
        var treino = await _treinoService.GetByIdAsync(id);
        if (treino == null)
            return NotFound();

        return Ok(treino);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<TreinoDto>>> GetByUsuarioId(int usuarioId)
    {
        var treinos = await _treinoService.GetByUsuarioIdAsync(usuarioId);
        return Ok(treinos);
    }

    [HttpGet("ranking")]
    public async Task<ActionResult<IEnumerable<TreinoDto>>> GetRanking([FromQuery] int top = 10)
    {
        var ranking = await _treinoService.GetRankingByCaloriasAsync(top);
        return Ok(ranking);
    }

    [HttpPost("cardio")]
    public async Task<ActionResult<TreinoDto>> CreateCardio(CreateTreinoCardioDto dto)
    {
        var treino = await _treinoService.CreateTreinoCardioAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = treino.Id }, treino);
    }

    [HttpPost("musculacao")]
    public async Task<ActionResult<TreinoDto>> CreateMusculacao(CreateTreinoMusculacaoDto dto)
    {
        var treino = await _treinoService.CreateTreinoMusculacaoAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = treino.Id }, treino);
    }

    [HttpPut("cardio/{id}")]
    public async Task<IActionResult> UpdateCardio(int id, UpdateTreinoCardioDto dto)
    {
        var result = await _treinoService.UpdateTreinoCardioAsync(id, dto);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPut("musculacao/{id}")]
    public async Task<IActionResult> UpdateMusculacao(int id, UpdateTreinoMusculacaoDto dto)
    {
        var result = await _treinoService.UpdateTreinoMusculacaoAsync(id, dto);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _treinoService.DeleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
