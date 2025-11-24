namespace FitLife.Application.DTOs;

public class UpdateHabitoDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Frequencia { get; set; } = string.Empty;
}
