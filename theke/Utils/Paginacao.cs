using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace theke.Utils
{
    public class Paginacao<T> : List<T>
    {
        public int PaginaInicial { get; private set; }
        public int TotalPaginas { get; private set; }

        public Paginacao(List<T> items, int count, int paginaInicial, int tamanhoPagina)
        {
            PaginaInicial = paginaInicial;
            TotalPaginas = (int)Math.Ceiling(count / (double)tamanhoPagina);

            this.AddRange(items);
        }

        public bool TemPaginaAnterior
        {
            get
            {
                return (PaginaInicial > 1);
            }
        }

        public bool TemProximaPagina
        {
            get
            {
                return (PaginaInicial < TotalPaginas);
            }
        }

        public static async Task<Paginacao<T>> Pagina(IQueryable<T> source, int paginaInicial, int tamanhoPagina)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((paginaInicial - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            return new Paginacao<T>(items, count, paginaInicial, tamanhoPagina);
        }
    }
}
