using FitLife.Application.DTOs;

namespace FitLife.Application.Interfaces;

public interface IHabitoService
{
    Task<HabitoDto?> GetByIdAsync(int id);
    Task<IEnumerable<HabitoDto>> GetByUsuarioIdAsync(int usuarioId);
    Task<HabitoDto> CreateAsync(CreateHabitoDto dto);
    Task<bool> DeleteAsync(int id);
}
