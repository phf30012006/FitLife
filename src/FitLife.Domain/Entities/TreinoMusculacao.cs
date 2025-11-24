namespace FitLife.Domain.Entities;

public class TreinoMusculacao : Treino
{
    public string GrupoMuscular { get; set; } = string.Empty;
    public int Series { get; set; }
    public int Repeticoes { get; set; }
    public double CargaKg { get; set; }
    
    public override int CalcularCalorias()
    {
        return (int)(Series * Repeticoes * 0.5);
    }
}
