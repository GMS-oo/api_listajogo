namespace jogos.DTOs
{
    public class JogoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Plataforma { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double Nota { get; set; }
        public decimal Valor { get; set; }
        public int UsuarioId { get; set; }
    }
}
