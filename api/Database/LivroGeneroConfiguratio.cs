using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.Models;

namespace api.Database
{
    internal class LivroGeneroConfiguratio : IEntityTypeConfiguration<LivroGenero>
    {
        public void Configure(EntityTypeBuilder<LivroGenero> builder)
        {

            builder.HasKey(t => new { t.LivroId, t.GeneroId });
            builder
                .HasOne(l => l.Livro)
                .WithMany(g => g.LivroGenero)
                .HasForeignKey(l => l.LivroId);

            builder
                .HasOne(g => g.Genero)
                .WithMany(l => l.LivroGenero)
                .HasForeignKey(g => g.GeneroId);
        }
    }
}