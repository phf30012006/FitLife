using FitLife.Application.DTOs;

namespace FitLife.Application.Interfaces;

public interface IRegistroAlimentacaoService
{
    Task<RegistroAlimentacaoDto?> GetByIdAsync(int id);
    Task<IEnumerable<RegistroAlimentacaoDto>> GetByUsuarioIdAsync(int usuarioId);
    Task<IEnumerable<RegistroAlimentacaoDto>> GetByUsuarioAndPeriodoAsync(int usuarioId, DateTime dataInicio, DateTime dataFim);
    Task<RegistroAlimentacaoDto> CreateAsync(CreateRegistroAlimentacaoDto dto);
    Task<bool> DeleteAsync(int id);
}
