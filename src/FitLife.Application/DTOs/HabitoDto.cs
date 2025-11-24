namespace FitLife.Application.DTOs;

public class HabitoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Frequencia { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public int UsuarioId { get; set; }
}
