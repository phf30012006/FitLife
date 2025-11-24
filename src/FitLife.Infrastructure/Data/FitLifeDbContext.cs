using FitLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Infrastructure.Data;

public class FitLifeDbContext : DbContext
{
    public FitLifeDbContext(DbContextOptions<FitLifeDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Treino> Treinos { get; set; }
    public DbSet<TreinoCardio> TreinosCardio { get; set; }
    public DbSet<TreinoMusculacao> TreinosMusculacao { get; set; }
    public DbSet<RegistroAlimentacao> RegistrosAlimentacao { get; set; }
    public DbSet<Habito> Habitos { get; set; }
    public DbSet<RegistroHabito> RegistrosHabito { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Treino>()
            .HasDiscriminator<string>("TipoTreino")
            .HasValue<TreinoCardio>("Cardio")
            .HasValue<TreinoMusculacao>("Musculacao");

        // Configuração de relacionamentos
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Treinos)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.RegistrosAlimentacao)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Habitos)
            .WithOne(h => h.Usuario)
            .HasForeignKey(h => h.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Habito>()
            .HasMany(h => h.Registros)
            .WithOne(r => r.Habito)
            .HasForeignKey(r => r.HabitoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Treino>()
            .HasIndex(t => t.UsuarioId);

        modelBuilder.Entity<RegistroAlimentacao>()
            .HasIndex(r => r.UsuarioId);
    }
}
