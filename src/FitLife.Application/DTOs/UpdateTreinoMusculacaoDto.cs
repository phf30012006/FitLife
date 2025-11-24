namespace FitLife.Application.DTOs;

public class UpdateTreinoMusculacaoDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int Duracao { get; set; }
    public string GrupoMuscular { get; set; } = string.Empty;
    public int Series { get; set; }
    public int Repeticoes { get; set; }
    public double CargaKg { get; set; }
}
