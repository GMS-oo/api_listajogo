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

        // Caminho ou URL da capa (armazenado no banco)
        public string CapaUrl { get; set; } = string.Empty;

    
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
