namespace FitLife.Domain.Entities;

public class RegistroAlimentacao
{
    public int Id { get; set; }
    public string Alimento { get; set; } = string.Empty;
    public int Calorias { get; set; }
    public double Proteinas { get; set; }
    public double Carboidratos { get; set; }
    public double Gorduras { get; set; }
    public DateTime DataRegistro { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
