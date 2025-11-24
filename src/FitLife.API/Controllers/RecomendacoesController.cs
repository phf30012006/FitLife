using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecomendacoesController : ControllerBase
{
    private readonly IRecomendacaoService _recomendacaoService;

    public RecomendacoesController(IRecomendacaoService recomendacaoService)
    {
        _recomendacaoService = recomendacaoService;
    }

    [HttpGet("usuario/{usuarioId}/relatorio")]
    public async Task<ActionResult<RelatorioUsuarioDto>> GetRelatorio(int usuarioId)
    {
        var relatorio = await _recomendacaoService.GerarRelatorioComSugestoesAsync(usuarioId);
        return Ok(relatorio);
    }

    [HttpGet("usuario/{usuarioId}/sugestoes")]
    public async Task<ActionResult<IEnumerable<SugestaoDto>>> GetSugestoes(int usuarioId)
    {
        var sugestoes = await _recomendacaoService.GerarSugestoesPersonalizadasAsync(usuarioId);
        return Ok(sugestoes);
    }
}
