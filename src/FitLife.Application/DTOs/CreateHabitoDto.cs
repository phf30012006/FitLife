namespace FitLife.Application.DTOs;

public class CreateHabitoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Frequencia { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
}
