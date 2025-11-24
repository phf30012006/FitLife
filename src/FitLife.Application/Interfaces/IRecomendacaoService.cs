using FitLife.Application.DTOs;

namespace FitLife.Application.Interfaces;

public interface IRecomendacaoService
{
    Task<RelatorioUsuarioDto> GerarRelatorioComSugestoesAsync(int usuarioId);
    Task<IEnumerable<SugestaoDto>> GerarSugestoesPersonalizadasAsync(int usuarioId);
}
