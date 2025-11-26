using Microsoft.EntityFrameworkCore;
using jogos.Models;

namespace jogos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuario 1 -> N Jogos
            modelBuilder.Entity<Jogo>()
                .HasOne(j => j.Usuario)
                .WithMany(u => u.Jogos)
                .HasForeignKey(j => j.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuario 1 -> N Avaliações
            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Jogo 1 -> N Avaliações
            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Jogo)
                .WithMany(j => j.Avaliacoes)
                .HasForeignKey(a => a.JogoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
