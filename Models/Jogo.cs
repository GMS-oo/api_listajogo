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
        public decimal Valor { get; set; }
        public string? CapaUrl { get; set; }

        // FK do usuario dono do jogo
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // Avaliações feitas por varios usuarios
        public List<Avaliacao>? Avaliacoes { get; set; }
    }
}
