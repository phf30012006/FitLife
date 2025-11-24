using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using FitLife.Domain.Entities;
using FitLife.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly FitLifeDbContext _context;

    public UsuarioService(FitLifeDbContext context)
    {
        _context = context;
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            DataCadastro = usuario.DataCadastro,
            Peso = usuario.Peso,
            Altura = usuario.Altura
        };
    }

    public async Task<UsuarioDto?> GetByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            DataCadastro = usuario.DataCadastro,
            Peso = usuario.Peso,
            Altura = usuario.Altura
        };
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            DataCadastro = u.DataCadastro,
            Peso = u.Peso,
            Altura = u.Altura
        });
    }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Peso = dto.Peso,
            Altura = dto.Altura,
            DataCadastro = DateTime.UtcNow
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        
        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            DataCadastro = usuario.DataCadastro,
            Peso = usuario.Peso,
            Altura = usuario.Altura
        };
    }

    public async Task<bool> UpdateAsync(int id, CreateUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Peso = dto.Peso;
        usuario.Altura = dto.Altura;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
