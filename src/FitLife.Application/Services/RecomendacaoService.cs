using FitLife.Application.DTOs;
using FitLife.Application.Interfaces;
using FitLife.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Application.Services;

public class RecomendacaoService : IRecomendacaoService
{
    private readonly FitLifeDbContext _context;

    public RecomendacaoService(FitLifeDbContext context)
    {
        _context = context;
    }

    public async Task<RelatorioUsuarioDto> GerarRelatorioComSugestoesAsync(int usuarioId)
    {
        var treinos = await _context.Treinos
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync();
        var alimentacao = await _context.RegistrosAlimentacao
            .Where(a => a.UsuarioId == usuarioId)
            .ToListAsync();
        
        var dataLimite = DateTime.UtcNow.AddDays(-7);
        var treinosRecentes = treinos.Where(t => t.DataCriacao >= dataLimite).ToList();
        
        var totalCalorias = treinos.Sum(t => t.CalcularCalorias());
        var totalCaloriasConsumidas = alimentacao.Sum(a => a.Calorias);
        
        var tipoMaisFrequente = treinos
            .GroupBy(t => t.GetType().Name)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()?.Key ?? "Nenhum";

        var relatorio = new RelatorioUsuarioDto
        {
            TotalTreinos = treinos.Count(),
            TotalCaloriasQueimadas = totalCalorias,
            TotalCaloriasConsumidas = totalCaloriasConsumidas,
            TreinosUltimos7Dias = treinosRecentes.Count,
            MediaCaloriasDiarias = alimentacao.Any() 
                ? alimentacao.Average(a => a.Calorias) 
                : 0,
            TipoTreinoMaisFrequente = tipoMaisFrequente.Replace("Treino", ""),
            Sugestoes = (await GerarSugestoesPersonalizadasAsync(usuarioId)).ToList()
        };

        return relatorio;
    }

    public async Task<IEnumerable<SugestaoDto>> GerarSugestoesPersonalizadasAsync(int usuarioId)
    {
        var sugestoes = new List<SugestaoDto>();
        
        var treinos = await _context.Treinos
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync();
        var alimentacao = await _context.RegistrosAlimentacao
            .Where(a => a.UsuarioId == usuarioId)
            .ToListAsync();
        var habitos = await _context.Habitos
            .Where(h => h.UsuarioId == usuarioId)
            .ToListAsync();

        var dataLimite7Dias = DateTime.UtcNow.AddDays(-7);
        var treinosRecentes = treinos.Where(t => t.DataCriacao >= dataLimite7Dias).ToList();

        // Treinos
        if (treinosRecentes.Count == 0)
        {
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Comece a se exercitar!",
                Mensagem = "Você não registrou nenhum treino nos últimos 7 dias. Que tal começar com uma caminhada leve de 20 minutos?",
                Tipo = "Treino",
                Prioridade = "Alta"
            });
        }
        else if (treinosRecentes.Count < 3)
        {
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Aumente sua frequência",
                Mensagem = $"Você treinou apenas {treinosRecentes.Count}x esta semana. O ideal é treinar pelo menos 3-4 vezes por semana.",
                Tipo = "Treino",
                Prioridade = "Media"
            });
        }
        else
        {
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Parabéns pela consistência!",
                Mensagem = $"Você treinou {treinosRecentes.Count}x esta semana. Continue assim!",
                Tipo = "Treino",
                Prioridade = "Baixa"
            });
        }

        var tiposTreino = treinos.Select(t => t.GetType().Name).Distinct().Count();
        if (tiposTreino < 2 && treinos.Any())
        {
            var tipoAtual = treinos.First().GetType().Name.Replace("Treino", "");
            var tipoSugerido = tipoAtual == "Cardio" ? "musculação" : "cardio";
            
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Varie seus treinos",
                Mensagem = $"Você está focando apenas em {tipoAtual}. Que tal adicionar treinos de {tipoSugerido} para resultados melhores?",
                Tipo = "Treino",
                Prioridade = "Media"
            });
        }

        // Alimentação
        var registrosAlimentacaoRecentes = alimentacao.Where(a => a.DataRegistro >= dataLimite7Dias).ToList();
        
        if (registrosAlimentacaoRecentes.Count == 0)
        {
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Registre sua alimentação",
                Mensagem = "Comece a registrar suas refeições para ter melhor controle sobre suas calorias e nutrientes.",
                Tipo = "Alimentacao",
                Prioridade = "Alta"
            });
        }
        else if (registrosAlimentacaoRecentes.Any())
        {
            var mediaProteinas = registrosAlimentacaoRecentes.Average(a => a.Proteinas);
            if (mediaProteinas < 20)
            {
                sugestoes.Add(new SugestaoDto
                {
                    Titulo = "Aumente a ingestão de proteínas",
                    Mensagem = $"Sua média de proteínas é {mediaProteinas:F1}g por refeição. Tente consumir pelo menos 20-30g por refeição.",
                    Tipo = "Alimentacao",
                    Prioridade = "Media"
                });
            }

            var totalCalorias = registrosAlimentacaoRecentes.Sum(a => a.Calorias);
            var caloriasQueimadas = treinosRecentes.Sum(t => t.CalcularCalorias());
            
            if (totalCalorias > caloriasQueimadas * 2 && treinosRecentes.Any())
            {
                sugestoes.Add(new SugestaoDto
                {
                    Titulo = "Atenção ao balanço calórico",
                    Mensagem = "Você está consumindo significativamente mais calorias do que queima. Considere ajustar sua dieta ou aumentar a intensidade dos treinos.",
                    Tipo = "Alimentacao",
                    Prioridade = "Alta"
                });
            }
        }

        // Hábitos
        if (!habitos.Any())
        {
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Crie hábitos saudáveis",
                Mensagem = "Experimente criar hábitos como 'Beber 2L de água', 'Dormir 8 horas' ou 'Meditar 10 minutos' para melhorar seus resultados.",
                Tipo = "Habito",
                Prioridade = "Media"
            });
        }

        // Descanso
        var treinosUltimos3Dias = treinos.Where(t => t.DataCriacao >= DateTime.UtcNow.AddDays(-3)).Count();
        if (treinosUltimos3Dias >= 3)
        {
            sugestoes.Add(new SugestaoDto
            {
                Titulo = "Não esqueça do descanso",
                Mensagem = "Você treinou 3 dias seguidos. Lembre-se que o descanso é essencial para recuperação muscular!",
                Tipo = "Treino",
                Prioridade = "Media"
            });
        }

        return sugestoes;
    }
}
