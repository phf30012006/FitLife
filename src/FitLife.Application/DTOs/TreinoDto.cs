namespace FitLife.Application.DTOs;

public class TreinoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Duracao { get; set; }
    public DateTime DataCriacao { get; set; }
    public int UsuarioId { get; set; }
    public string TipoTreino { get; set; } = string.Empty;
    public int CaloriasEstimadas { get; set; }
}
