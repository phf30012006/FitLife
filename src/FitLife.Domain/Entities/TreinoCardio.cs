namespace FitLife.Domain.Entities;

public class TreinoCardio : Treino
{
    public double DistanciaKm { get; set; }
    public int FrequenciaCardiacaMedia { get; set; }
    
    public override int CalcularCalorias()
    {
        return (int)(DistanciaKm * 60);
    }
}
