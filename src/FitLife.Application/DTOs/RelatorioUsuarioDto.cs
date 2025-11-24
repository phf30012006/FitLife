namespace FitLife.Application.DTOs;

public class RelatorioUsuarioDto
{
    public int TotalTreinos { get; set; }
    public int TotalCaloriasQueimadas { get; set; }
    public int TotalCaloriasConsumidas { get; set; }
    public int TreinosUltimos7Dias { get; set; }
    public double MediaCaloriasDiarias { get; set; }
    public string TipoTreinoMaisFrequente { get; set; } = string.Empty;
    public List<SugestaoDto> Sugestoes { get; set; } = new List<SugestaoDto>();
}
