using System;
using Microsoft.AspNetCore.Mvc;
using api.Database;
using api.Models;
using api.Utils;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivrosController : ControllerBase
    {
        // --- Atributos -------------------------------------------------------
        private readonly DBContext database;
        

        // --- Construtor ------------------------------------------------------
        public LivrosController(DBContext database)
        {
            this.database = database;
        }

        [HttpGet("livro/{livroId}")]
        // --- Buscar ----------------------------------------------------------
        public IActionResult Buscar(int livroId = 0)
        {

            try
            {
                var usuarioId = HttpContext.User.Claims.First(c => c.Type.ToString().Equals("id")).Value;
                if (livroId == 0)
                {
                    throw new Exception("Livro não encontrado.");
                }

                var livro = this.database.Livros
                .Include(l => l.Autor)
                .Include(l => l.Editora)
                .Include(l => l.Idioma)
                .Include(l => l.Usuario)
                .Include(l => l.LivroGenero)
                .ThenInclude(l => l.Genero)
                .Where(l => l.Usuario.Id == Int32.Parse(usuarioId) && l.Id == livroId)
                .First();

                // --- Retorno ---
                return Ok(new { ok = "1", retorno = new { livro } });
            } catch (Exception e)
            {
                // --- Retorno ---
                return BadRequest("Livro não encontrado.");
            }

        }

        [HttpGet("{pagina?}")]
        // --- Index -----------------------------------------------------------
        public async Task<IActionResult> IndexAsync(int pagina)
        {

            var usuarioId = HttpContext.User.Claims.First(c => c.Type.ToString().Equals("id")).Value;

            if (pagina == 0)
            {
                pagina = 1;
            }

            var consulta = this.database.Livros
                .Include(l => l.Autor)
                .Include(l => l.Editora)
                .Include(l => l.Idioma)
                .Include(l => l.Usuario)
                .Include(l => l.LivroGenero)
                .ThenInclude(l => l.Genero)
                .Where(l => l.Usuario.Id == Int32.Parse(usuarioId))
                .OrderBy(l => l.Titulo);

            int tamanhoPagina = 3;
            var livros = await Paginacao<Livro>.Pagina(consulta.AsNoTracking(), pagina, tamanhoPagina);

            // --- Retorno ---
            return Ok(new { ok = "1", retorno = new { livros } });

        }

        [HttpPost("buscar")]
        // --- Buscar ----------------------------------------------------------
        public IActionResult Buscar([FromBody] Livro livro)
        {

            try
            {
                var usuarioId = HttpContext.User.Claims.First(c => c.Type.ToString().Equals("id")).Value;
                var livros = this.database.Livros
                    .Include(l => l.Autor)
                    .Include(l => l.Editora)
                    .Include(l => l.Idioma)
                    .Include(l => l.Usuario)
                    .Include(l => l.LivroGenero)
                    .ThenInclude(l => l.Genero)
                    .Where(l => l.Usuario.Id == Int32.Parse(usuarioId) && l.Titulo.ToLower().Contains(livro.Titulo.ToLower()))
                    .OrderBy(l => l.Titulo)
                    .ToList();

                if (livros.Count() == 0)
                {
                    throw new Exception("");
                }

                // --- Retorno ---
                return Ok(new { ok = "1", retorno = new { livros } });
            }
            catch (Exception e)
            {
                // --- Retorno ---
                return BadRequest("Nenhum livro encontrado.");
            }
        }

        [HttpDelete("livro/{livroId}")]
        // --- Apagar ----------------------------------------------------------
        public IActionResult Apagar(int livroId)
        {

            try
            {
                var usuarioId = HttpContext.User.Claims.First(c => c.Type.ToString().Equals("id")).Value;
                var livro = this.database.Livros
                .Where(l => l.Usuario.Id == Int32.Parse(usuarioId) && l.Id == livroId)
                .First();
                this.database.Remove(livro);
                this.database.SaveChanges();

                // --- Retorno ---
                return Ok(new { ok = "1", retorno = "Livro apagado com sucesso."});
            } catch (Exception e)
            {
                // --- Retorno ---
                return BadRequest("Livro não encontrado.");
            }
            
        }

        [HttpPost]
        // --- Cadastrar -------------------------------------------------------
        public async Task<IActionResult> CadastrarAsync([FromBody] Livro livro)
        {

            try
            {

                // --- Verifica se já não existe no banco de dados --
                var usuarioId = HttpContext.User.Claims.First(c => c.Type.ToString().Equals("id")).Value;
                var livroBanco = this.database.Livros.Where(l => l.Isbn.Equals(livro.Isbn) && l.Usuario.Id == Int32.Parse(usuarioId)).FirstOrDefault();
                if (livroBanco != null)
                {
                    return Ok(new { ok = "0", msg = "Livro já cadastrado" });
                }

                var retorno = await Skoob.Buscar(livro.Isbn);

                // --- Idioma ---
                var idioma = this.database.Idiomas.Where(i => i.Descricao.Equals(retorno.Idioma.Descricao)).FirstOrDefault();
                if (idioma == null)
                {
                    this.database.Idiomas.Add(retorno.Idioma);
                }
                else
                {
                    retorno.Idioma = idioma;
                }

                // --- Editora ---
                var editora = this.database.Editoras.Where(e => e.Descricao.Equals(retorno.Editora.Descricao)).FirstOrDefault();
                if (editora == null)
                {
                    this.database.Editoras.Add(retorno.Editora);
                }
                else
                {
                    retorno.Editora = editora;
                }

                // --- Autor ---
                var autor = this.database.Autores.Where(a => a.Descricao.Equals(retorno.Autor.Descricao)).FirstOrDefault();
                if (autor == null)
                {
                    this.database.Autores.Add(retorno.Autor);
                }
                else
                {
                    retorno.Autor = autor;
                }

                // --- Usuário ---
                var usuario = this.database.Usuarios.First(u => u.Id == Int32.Parse(usuarioId));
                retorno.Usuario = usuario;

                // --- Salva Gêneros ---
                foreach (var genero in retorno.Generos)
                {
                    var generoBD = this.database.Generos.Where(g => g.Descricao.Equals(genero.Descricao)).FirstOrDefault();
                    if (generoBD == null)
                    {
                        generoBD = genero;
                    }

                    this.database.Add(new LivroGenero { Livro = retorno, Genero = generoBD });
                }

                // --- Salva livro ---
                this.database.Livros.Add(retorno);
                this.database.SaveChanges();

                // --- Retorno ---
                Response.StatusCode = 201;
                return new ObjectResult(new { ok = "1", retorno = new { retorno } });

            }
            catch (Exception e)
            {
                // --- Se tiver algum erro ---
                Console.WriteLine(e.StackTrace);
                return BadRequest(new { ok = "0", msg = e.Message });
            }

        }

    }
}
