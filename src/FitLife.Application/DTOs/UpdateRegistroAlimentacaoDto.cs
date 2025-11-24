namespace FitLife.Application.DTOs;

public class UpdateRegistroAlimentacaoDto
{
    public string Alimento { get; set; } = string.Empty;
    public int Calorias { get; set; }
    public double Proteinas { get; set; }
    public double Carboidratos { get; set; }
    public double Gorduras { get; set; }
}
