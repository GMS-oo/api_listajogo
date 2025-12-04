using Microsoft.EntityFrameworkCore;
using jogos.Models;

namespace jogos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = default!;
        public DbSet<Jogo> Jogos { get; set; } = default!;

    public DbSet<Comentario> Comentarios { get; set; }

    }
}
