namespace FitLife.Application.DTOs;

public class CreateTreinoCardioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Duracao { get; set; }
    public int UsuarioId { get; set; }
    public double DistanciaKm { get; set; }
    public int FrequenciaCardiacaMedia { get; set; }
}
