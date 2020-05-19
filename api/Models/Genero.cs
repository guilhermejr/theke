using System.Collections.Generic;

namespace api.Models
{
    public class Genero : ModeloPadrao
    {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<LivroGenero> LivroGenero { get; set; } 

    }
}