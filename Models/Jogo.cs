using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jogos.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Plataforma { get; set; }
        public string Descricao { get; set; }
        public double Nota { get; set; }
        public double Valor { get; set; }
    }
}