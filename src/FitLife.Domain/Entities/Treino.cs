namespace FitLife.Domain.Entities;

public abstract class Treino
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Duracao { get; set; }
    public DateTime DataCriacao { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public abstract int CalcularCalorias();
}
