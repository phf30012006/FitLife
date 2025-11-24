using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using FitLife.Domain.Entities;
using FitLife.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Application.Services;

public class TreinoService : ITreinoService
{
    private readonly FitLifeDbContext _context;

    public TreinoService(FitLifeDbContext context)
    {
        _context = context;
    }

    public async Task<TreinoDto?> GetByIdAsync(int id)
    {
        var treino = await _context.Treinos.FindAsync(id);
        if (treino == null) return null;

        return new TreinoDto
        {
            Id = treino.Id,
            Nome = treino.Nome,
            Descricao = treino.Descricao,
            Duracao = treino.Duracao,
            DataCriacao = treino.DataCriacao,
            UsuarioId = treino.UsuarioId,
            TipoTreino = treino.GetType().Name.Replace("Treino", ""),
            CaloriasEstimadas = treino.CalcularCalorias()
        };
    }

    public async Task<IEnumerable<TreinoDto>> GetAllAsync()
    {
        var treinos = await _context.Treinos.ToListAsync();
        return treinos.Select(t => new TreinoDto
        {
            Id = t.Id,
            Nome = t.Nome,
            Descricao = t.Descricao,
            Duracao = t.Duracao,
            DataCriacao = t.DataCriacao,
            UsuarioId = t.UsuarioId,
            TipoTreino = t.GetType().Name.Replace("Treino", ""),
            CaloriasEstimadas = t.CalcularCalorias()
        });
    }

    public async Task<IEnumerable<TreinoDto>> GetByUsuarioIdAsync(int usuarioId)
    {
        var treinos = await _context.Treinos
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync();
        return treinos.Select(t => new TreinoDto
        {
            Id = t.Id,
            Nome = t.Nome,
            Descricao = t.Descricao,
            Duracao = t.Duracao,
            DataCriacao = t.DataCriacao,
            UsuarioId = t.UsuarioId,
            TipoTreino = t.GetType().Name.Replace("Treino", ""),
            CaloriasEstimadas = t.CalcularCalorias()
        });
    }

    public async Task<TreinoDto> CreateTreinoCardioAsync(CreateTreinoCardioDto dto)
    {
        var treino = new TreinoCardio
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Duracao = dto.Duracao,
            UsuarioId = dto.UsuarioId,
            DistanciaKm = dto.DistanciaKm,
            FrequenciaCardiacaMedia = dto.FrequenciaCardiacaMedia,
            DataCriacao = DateTime.UtcNow
        };

        _context.Treinos.Add(treino);
        await _context.SaveChangesAsync();
        
        return new TreinoDto
        {
            Id = treino.Id,
            Nome = treino.Nome,
            Descricao = treino.Descricao,
            Duracao = treino.Duracao,
            DataCriacao = treino.DataCriacao,
            UsuarioId = treino.UsuarioId,
            TipoTreino = "Cardio",
            CaloriasEstimadas = treino.CalcularCalorias()
        };
    }

    public async Task<TreinoDto> CreateTreinoMusculacaoAsync(CreateTreinoMusculacaoDto dto)
    {
        var treino = new TreinoMusculacao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Duracao = dto.Duracao,
            UsuarioId = dto.UsuarioId,
            GrupoMuscular = dto.GrupoMuscular,
            Series = dto.Series,
            Repeticoes = dto.Repeticoes,
            CargaKg = dto.CargaKg,
            DataCriacao = DateTime.UtcNow
        };

        _context.Treinos.Add(treino);
        await _context.SaveChangesAsync();
        
        return new TreinoDto
        {
            Id = treino.Id,
            Nome = treino.Nome,
            Descricao = treino.Descricao,
            Duracao = treino.Duracao,
            DataCriacao = treino.DataCriacao,
            UsuarioId = treino.UsuarioId,
            TipoTreino = "Musculacao",
            CaloriasEstimadas = treino.CalcularCalorias()
        };
    }

    public async Task<bool> UpdateTreinoCardioAsync(int id, UpdateTreinoCardioDto dto)
    {
        var treino = await _context.Treinos.FindAsync(id);
        if (treino == null || treino is not TreinoCardio) return false;

        var treinoCardio = (TreinoCardio)treino;
        treinoCardio.Nome = dto.Nome;
        treinoCardio.Descricao = dto.Descricao;
        treinoCardio.Duracao = dto.Duracao;
        treinoCardio.DistanciaKm = dto.DistanciaKm;
        treinoCardio.FrequenciaCardiacaMedia = dto.FrequenciaCardiacaMedia;

        _context.Treinos.Update(treinoCardio);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTreinoMusculacaoAsync(int id, UpdateTreinoMusculacaoDto dto)
    {
        var treino = await _context.Treinos.FindAsync(id);
        if (treino == null || treino is not TreinoMusculacao) return false;

        var treinoMusculacao = (TreinoMusculacao)treino;
        treinoMusculacao.Nome = dto.Nome;
        treinoMusculacao.Descricao = dto.Descricao;
        treinoMusculacao.Duracao = dto.Duracao;
        treinoMusculacao.GrupoMuscular = dto.GrupoMuscular;
        treinoMusculacao.Series = dto.Series;
        treinoMusculacao.Repeticoes = dto.Repeticoes;
        treinoMusculacao.CargaKg = dto.CargaKg;

        _context.Treinos.Update(treinoMusculacao);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var treino = await _context.Treinos.FindAsync(id);
        if (treino == null) return false;

        _context.Treinos.Remove(treino);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<TreinoDto>> GetRankingByCaloriasAsync(int top = 10)
    {
        var treinos = await _context.Treinos
            .ToListAsync();
        
        return treinos
            .OrderByDescending(t => t.CalcularCalorias())
            .Take(top)
            .Select(t => new TreinoDto
            {
                Id = t.Id,
                Nome = t.Nome,
                Descricao = t.Descricao,
                Duracao = t.Duracao,
                DataCriacao = t.DataCriacao,
                UsuarioId = t.UsuarioId,
                TipoTreino = t.GetType().Name.Replace("Treino", ""),
                CaloriasEstimadas = t.CalcularCalorias()
            });
    }
}
