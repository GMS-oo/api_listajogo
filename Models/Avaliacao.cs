namespace jogos.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public double Nota { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        // FK do jogo avaliado
        public int JogoId { get; set; }
        public Jogo? Jogo { get; set; }

        // FK do usuario que fez a avaliação
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
