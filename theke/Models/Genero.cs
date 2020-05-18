using System.Collections.Generic;

namespace theke.Models
{
    public class Genero
    {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<LivroGenero> LivroGenero { get; set; } 

    }
}