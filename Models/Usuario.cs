// Models/Usuario.cs

using System.Text.Json.Serialization; // <--- NOVO USING AQUI
using System.Collections.Generic;

namespace jogos.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        // Adicionando [JsonIgnore] para evitar o ciclo infinito
        [JsonIgnore] // <--- ADICIONE ESTA LINHA AQUI
        public List<Jogo>? Jogos { get; set; }
    }
}