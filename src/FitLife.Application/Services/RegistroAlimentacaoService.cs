using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using FitLife.Domain.Entities;
using FitLife.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Application.Services;

public class RegistroAlimentacaoService : IRegistroAlimentacaoService
{
    private readonly FitLifeDbContext _context;

    public RegistroAlimentacaoService(FitLifeDbContext context)
    {
        _context = context;
    }

    public async Task<RegistroAlimentacaoDto?> GetByIdAsync(int id)
    {
        var registro = await _context.RegistrosAlimentacao.FindAsync(id);
        if (registro == null) return null;

        return new RegistroAlimentacaoDto
        {
            Id = registro.Id,
            Alimento = registro.Alimento,
            Calorias = registro.Calorias,
            Proteinas = registro.Proteinas,
            Carboidratos = registro.Carboidratos,
            Gorduras = registro.Gorduras,
            DataRegistro = registro.DataRegistro,
            UsuarioId = registro.UsuarioId
        };
    }

    public async Task<IEnumerable<RegistroAlimentacaoDto>> GetByUsuarioIdAsync(int usuarioId)
    {
        var registros = await _context.RegistrosAlimentacao
            .Where(r => r.UsuarioId == usuarioId)
            .ToListAsync();
        return registros.Select(r => new RegistroAlimentacaoDto
        {
            Id = r.Id,
            Alimento = r.Alimento,
            Calorias = r.Calorias,
            Proteinas = r.Proteinas,
            Carboidratos = r.Carboidratos,
            Gorduras = r.Gorduras,
            DataRegistro = r.DataRegistro,
            UsuarioId = r.UsuarioId
        });
    }

    public async Task<IEnumerable<RegistroAlimentacaoDto>> GetByUsuarioAndPeriodoAsync(int usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        var registros = await _context.RegistrosAlimentacao
            .Where(r => r.UsuarioId == usuarioId && r.DataRegistro >= dataInicio && r.DataRegistro <= dataFim)
            .ToListAsync();
        return registros.Select(r => new RegistroAlimentacaoDto
        {
            Id = r.Id,
            Alimento = r.Alimento,
            Calorias = r.Calorias,
            Proteinas = r.Proteinas,
            Carboidratos = r.Carboidratos,
            Gorduras = r.Gorduras,
            DataRegistro = r.DataRegistro,
            UsuarioId = r.UsuarioId
        });
    }

    public async Task<RegistroAlimentacaoDto> CreateAsync(CreateRegistroAlimentacaoDto dto)
    {
        var registro = new RegistroAlimentacao
        {
            Alimento = dto.Alimento,
            Calorias = dto.Calorias,
            Proteinas = dto.Proteinas,
            Carboidratos = dto.Carboidratos,
            Gorduras = dto.Gorduras,
            UsuarioId = dto.UsuarioId,
            DataRegistro = DateTime.UtcNow
        };

        _context.RegistrosAlimentacao.Add(registro);
        await _context.SaveChangesAsync();
        
        return new RegistroAlimentacaoDto
        {
            Id = registro.Id,
            Alimento = registro.Alimento,
            Calorias = registro.Calorias,
            Proteinas = registro.Proteinas,
            Carboidratos = registro.Carboidratos,
            Gorduras = registro.Gorduras,
            DataRegistro = registro.DataRegistro,
            UsuarioId = registro.UsuarioId
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateRegistroAlimentacaoDto dto)
    {
        var registro = await _context.RegistrosAlimentacao.FindAsync(id);
        if (registro == null) return false;

        registro.Alimento = dto.Alimento;
        registro.Calorias = dto.Calorias;
        registro.Proteinas = dto.Proteinas;
        registro.Carboidratos = dto.Carboidratos;
        registro.Gorduras = dto.Gorduras;

        _context.RegistrosAlimentacao.Update(registro);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var registro = await _context.RegistrosAlimentacao.FindAsync(id);
        if (registro == null) return false;

        _context.RegistrosAlimentacao.Remove(registro);
        await _context.SaveChangesAsync();
        return true;
    }
}
