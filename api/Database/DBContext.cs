using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using api.Models;

namespace api.Database
{
    public class DBContext : DbContext
    {

        #region DefineLoggerFactory
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        #endregion

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LivroGenero> LivroGenero { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        #region RegisterLoggerFactory
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LivroGeneroConfiguratio());
        }
    }
}
