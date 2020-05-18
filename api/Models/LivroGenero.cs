using System;
namespace theke.Models
{
    public class LivroGenero
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; }

        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}
