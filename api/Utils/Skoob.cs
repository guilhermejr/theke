using System;
using System.Collections.Generic;
using System.Net.Http;
using AngleSharp;
using System.Threading.Tasks;
using AngleSharp.Dom;
using api.Models;
using System.Text.RegularExpressions;

namespace api.Utils
{
    public class Skoob
    {
        // --- Atributos -------------------------------------------------------

        // --- Construtor ------------------------------------------------------
        public Skoob()
        {
        }

        // --- Buscar -----------------------------------------------------------
        public static async Task<Livro> Buscar(string isbnConsulta)
        {

            // --- Submete o formulário de pesquisa ---
            IList<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>> {
                { new KeyValuePair<string, string>("data[Busca][tag]", isbnConsulta) },
            };

            HttpClient client = new HttpClient();
            var response = client.PostAsync("https://www.skoob.com.br/livro/lista", new FormUrlEncodedContent(nameValueCollection)).Result;
            var html = response.Content.ReadAsStringAsync().Result;

            // --- Lê HTML da pesquisa ---
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            var pesquisa = await context.OpenAsync(req => req.Content(html));

            // --- Verifica se achou o ISBN pesquisado ---
            var alert = pesquisa.QuerySelectorAll(".alert");
            if (alert.Length == 1)
            {
                throw new Exception("Livro não encontrado!");
            }

            // --- Monta o link do livro ---
            var link = pesquisa.QuerySelector(".detalhes > a").GetAttribute("href");
            var linkCompleto = "https://www.skoob.com.br" + link;

            // --- Lê HTML do livro ---
            var livroHTML = await context.OpenAsync(linkCompleto);

            // --- Capa ---
            string capaR = string.Empty;
            capaR = livroHTML.QuerySelector(".capa-link-item > img").GetAttribute("src").Trim();

            // --- Titulo ---
            string tituloR = string.Empty;
            tituloR = livroHTML.QuerySelector(".sidebar-titulo").Text().Trim();

            // ---Subtitulo-- -
            string subTituloR = string.Empty;
            try
            {
                subTituloR = livroHTML.QuerySelector(".sidebar-subtitulo").Text().Trim();
            }
            catch (Exception e)
            {
            }

            // --- ISBN ---
            string isbnR = string.Empty;
            isbnR = livroHTML.QuerySelector(".sidebar-desc > span").Text().Trim();

            // --- Descrição ---
            string descricaoR = string.Empty;
            descricaoR = livroHTML.QuerySelector("p[itemprop=description]").Text().Trim();

            // --- Genero ---
            string[] generosR = livroHTML.QuerySelector(".pg-livro-generos").Text().Trim().Split(" / ");

            // --- Autor ---
            string autorR = string.Empty;
            autorR = livroHTML.QuerySelector("#pg-livro-menu-principal-container > a").Text().Trim();

            // --- Editora ---
            string editoraR = string.Empty;
            editoraR = Regex.Replace(livroHTML.QuerySelector(".sidebar-desc").ToHtml().Split("<br>")[4].Split(":")[1].Trim(), @"<(.|\n)*?>", string.Empty);

            // --- Idioma ---
            string idiomaR = string.Empty;
            idiomaR = livroHTML.QuerySelector(".sidebar-desc").ToHtml().Split("<br>")[3].Split(":")[1].Trim();

            // --- Ano ---
            string anoR = string.Empty;
            anoR = livroHTML.QuerySelector(".sidebar-desc").ToHtml().Split("<br>")[2].Split(" / ")[0].Split(":")[1].Trim();

            // --- Páginas ---
            string paginasR = string.Empty;
            paginasR = livroHTML.QuerySelector(".sidebar-desc").ToHtml().Split("<br>")[2].Split(" / ")[1].Split(":")[1].Trim();

            var livro = new Livro();
            var autor = new Autor();
            autor.Descricao = autorR;
            var editora = new Editora();
            editora.Descricao = editoraR;
            var idioma = new Idioma();
            idioma.Descricao = idiomaR;
            var genero = new List<Genero>();

            foreach (var g in generosR)
            {
                var gen = new Genero();
                gen.Descricao = g;

                genero.Add(gen);
            }

            livro.Capa = capaR;
            livro.Titulo = tituloR;
            livro.SubTitulo = subTituloR;
            livro.Isbn = isbnR;
            livro.Descricao = descricaoR;
            livro.Autor = autor;
            livro.Editora = editora;
            livro.Idioma = idioma;
            livro.Ano = anoR;
            livro.Paginas = paginasR;
            livro.Generos = genero;

            return livro;
        }
    }
}
