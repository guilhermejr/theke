﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Database;

namespace api.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("api.Models.Editora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Editoras");
                });

            modelBuilder.Entity("api.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("api.Models.Idioma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Idiomas");
                });

            modelBuilder.Entity("api.Models.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Ano")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("AutorId")
                        .HasColumnType("int");

                    b.Property<string>("Capa")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("EditoraId")
                        .HasColumnType("int");

                    b.Property<int?>("IdiomaId")
                        .HasColumnType("int");

                    b.Property<string>("Isbn")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Paginas")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SubTitulo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("EditoraId");

                    b.HasIndex("IdiomaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("api.Models.LivroGenero", b =>
                {
                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("LivroId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.ToTable("LivroGenero");
                });

            modelBuilder.Entity("api.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("api.Models.Livro", b =>
                {
                    b.HasOne("api.Models.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId");

                    b.HasOne("api.Models.Editora", "Editora")
                        .WithMany()
                        .HasForeignKey("EditoraId");

                    b.HasOne("api.Models.Idioma", "Idioma")
                        .WithMany()
                        .HasForeignKey("IdiomaId");

                    b.HasOne("api.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("api.Models.LivroGenero", b =>
                {
                    b.HasOne("api.Models.Genero", "Genero")
                        .WithMany("LivroGenero")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Livro", "Livro")
                        .WithMany("LivroGenero")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
