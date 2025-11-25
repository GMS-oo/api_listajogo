using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace jogos.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Plataforma { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Nota { get; set; }
        public double Valor { get; set; }

        // Cover image URL or local path (ex: /images/elden.jpg)
        public string CapaUrl { get; set; } = string.Empty;

        // Relacionamento com usuário (opcional: proprietario do jogo)
        public int? UsuarioId { get; set; } // opcional
        public Usuario? Usuario { get; set; }

        // Avaliações feitas por usuários
        public List<Avaliacao>? Avaliacoes { get; set; }
    }
}
