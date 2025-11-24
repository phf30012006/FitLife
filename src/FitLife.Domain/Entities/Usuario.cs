namespace FitLife.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    
    public ICollection<Treino> Treinos { get; set; } = new List<Treino>();
    public ICollection<RegistroAlimentacao> RegistrosAlimentacao { get; set; } = new List<RegistroAlimentacao>();
    public ICollection<Habito> Habitos { get; set; } = new List<Habito>();
}
