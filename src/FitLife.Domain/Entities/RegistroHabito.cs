namespace FitLife.Domain.Entities;

public class RegistroHabito
{
    public int Id { get; set; }
    public int HabitoId { get; set; }
    public Habito Habito { get; set; } = null!;
    public DateTime Data { get; set; }
    public bool Concluido { get; set; }
    public string? Observacao { get; set; }
}
