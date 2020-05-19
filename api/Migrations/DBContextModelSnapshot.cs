﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using theke.Database;

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

            modelBuilder.Entity("theke.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("theke.Models.Editora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Editoras");
                });

            modelBuilder.Entity("theke.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("theke.Models.Idioma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Idiomas");
                });

            modelBuilder.Entity("theke.Models.Livro", b =>
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

            modelBuilder.Entity("theke.Models.LivroGenero", b =>
                {
                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("LivroId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.ToTable("LivroGenero");
                });

            modelBuilder.Entity("theke.Models.Usuario", b =>
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

            modelBuilder.Entity("theke.Models.Livro", b =>
                {
                    b.HasOne("theke.Models.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId");

                    b.HasOne("theke.Models.Editora", "Editora")
                        .WithMany()
                        .HasForeignKey("EditoraId");

                    b.HasOne("theke.Models.Idioma", "Idioma")
                        .WithMany()
                        .HasForeignKey("IdiomaId");

                    b.HasOne("theke.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("theke.Models.LivroGenero", b =>
                {
                    b.HasOne("theke.Models.Genero", "Genero")
                        .WithMany("LivroGenero")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("theke.Models.Livro", "Livro")
                        .WithMany("LivroGenero")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
