namespace FitLife.Application.DTOs;

public class CreateUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public double Peso { get; set; }
    public double Altura { get; set; }
}
