namespace FitLife.Application.DTOs;

public class CreateTreinoMusculacaoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Duracao { get; set; }
    public int UsuarioId { get; set; }
    public string GrupoMuscular { get; set; } = string.Empty;
    public int Series { get; set; }
    public int Repeticoes { get; set; }
    public double CargaKg { get; set; }
}
