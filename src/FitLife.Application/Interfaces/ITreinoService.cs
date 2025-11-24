using FitLife.Application.DTOs;

namespace FitLife.Application.Interfaces;

public interface ITreinoService
{
    Task<TreinoDto?> GetByIdAsync(int id);
    Task<IEnumerable<TreinoDto>> GetAllAsync();
    Task<IEnumerable<TreinoDto>> GetByUsuarioIdAsync(int usuarioId);
    Task<TreinoDto> CreateTreinoCardioAsync(CreateTreinoCardioDto dto);
    Task<TreinoDto> CreateTreinoMusculacaoAsync(CreateTreinoMusculacaoDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<TreinoDto>> GetRankingByCaloriasAsync(int top = 10);
}
