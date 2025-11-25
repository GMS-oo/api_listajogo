using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jogos.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int JogoId { get; set; }
        public Jogo Jogo { get; set; } = null!;

        public int Nota { get; set; } // 0..10
        public string? Comentario { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
    }
}
