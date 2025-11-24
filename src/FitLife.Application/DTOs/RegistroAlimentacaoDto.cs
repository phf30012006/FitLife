namespace FitLife.Application.DTOs;

public class RegistroAlimentacaoDto
{
    public int Id { get; set; }
    public string Alimento { get; set; } = string.Empty;
    public int Calorias { get; set; }
    public double Proteinas { get; set; }
    public double Carboidratos { get; set; }
    public double Gorduras { get; set; }
    public DateTime DataRegistro { get; set; }
    public int UsuarioId { get; set; }
}
