using FitLife.Application.DTOs;

namespace FitLife.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDto?> GetByIdAsync(int id);
    Task<UsuarioDto?> GetByEmailAsync(string email);
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
    Task<bool> UpdateAsync(int id, CreateUsuarioDto dto);
    Task<bool> DeleteAsync(int id);
}
