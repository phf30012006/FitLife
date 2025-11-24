using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using FitLife.Domain.Entities;
using FitLife.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Application.Services;

public class HabitoService : IHabitoService
{
    private readonly FitLifeDbContext _context;

    public HabitoService(FitLifeDbContext context)
    {
        _context = context;
    }

    public async Task<HabitoDto?> GetByIdAsync(int id)
    {
        var habito = await _context.Habitos.FindAsync(id);
        if (habito == null) return null;

        return new HabitoDto
        {
            Id = habito.Id,
            Nome = habito.Nome,
            Descricao = habito.Descricao,
            Frequencia = habito.Frequencia,
            DataInicio = habito.DataInicio,
            UsuarioId = habito.UsuarioId
        };
    }

    public async Task<IEnumerable<HabitoDto>> GetByUsuarioIdAsync(int usuarioId)
    {
        var habitos = await _context.Habitos
            .Where(h => h.UsuarioId == usuarioId)
            .ToListAsync();
        return habitos.Select(h => new HabitoDto
        {
            Id = h.Id,
            Nome = h.Nome,
            Descricao = h.Descricao,
            Frequencia = h.Frequencia,
            DataInicio = h.DataInicio,
            UsuarioId = h.UsuarioId
        });
    }

    public async Task<HabitoDto> CreateAsync(CreateHabitoDto dto)
    {
        var habito = new Habito
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Frequencia = dto.Frequencia,
            UsuarioId = dto.UsuarioId,
            DataInicio = DateTime.UtcNow
        };

        _context.Habitos.Add(habito);
        await _context.SaveChangesAsync();
        
        return new HabitoDto
        {
            Id = habito.Id,
            Nome = habito.Nome,
            Descricao = habito.Descricao,
            Frequencia = habito.Frequencia,
            DataInicio = habito.DataInicio,
            UsuarioId = habito.UsuarioId
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateHabitoDto dto)
    {
        var habito = await _context.Habitos.FindAsync(id);
        if (habito == null) return false;

        habito.Nome = dto.Nome;
        habito.Descricao = dto.Descricao;
        habito.Frequencia = dto.Frequencia;

        _context.Habitos.Update(habito);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var habito = await _context.Habitos.FindAsync(id);
        if (habito == null) return false;

        _context.Habitos.Remove(habito);
        await _context.SaveChangesAsync();
        return true;
    }
}
