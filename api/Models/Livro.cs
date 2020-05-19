using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Livro : ModeloPadrao
    {

        public int Id { get; set; }
        public string Capa { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Isbn { get; set; }
        public string Descricao { get; set; }
        public List<LivroGenero> LivroGenero { get; set; }
        public Autor Autor { get; set; }
        public Editora Editora { get; set; }
        public Idioma Idioma { get; set; }
        public string Ano { get; set; }
        public string Paginas { get; set; }
        public Usuario Usuario { get; set; }

        [NotMapped]
        public List<Genero> Generos { get; set; }

    }
}
