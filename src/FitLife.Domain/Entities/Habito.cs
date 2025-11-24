namespace FitLife.Domain.Entities;

public class Habito
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Frequencia { get; set; } = string.Empty; 
    public DateTime DataInicio { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public ICollection<RegistroHabito> Registros { get; set; } = new List<RegistroHabito>();
}
