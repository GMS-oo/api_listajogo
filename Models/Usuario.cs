namespace jogos.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        // Um usuário pode ter varios jogos cadastrados
        public List<Jogo>? Jogos { get; set; }

        // Um usuário pode escrever várias avaliações
        public List<Avaliacao>? Avaliacoes { get; set; }
    }
}
