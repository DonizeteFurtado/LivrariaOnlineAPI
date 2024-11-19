using LivrariaOnlineAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaOnlineAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
    
    public DbSet<LivroModel> Livros { get; set; }
    public DbSet<GeneroModel> Generos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurando LivroModel
        modelBuilder.Entity<LivroModel>()
            .HasKey(l => l.Id); // Define a chave primária de Livro

        modelBuilder.Entity<LivroModel>()
            .HasOne(l => l.Genero) // Um Livro tem um Gênero
            .WithMany() // Um Gênero pode ter muitos Livros
            .HasForeignKey(l => l.GeneroId); // Define GeneroId como chave estrangeira

        // Configurando GeneroModel
        modelBuilder.Entity<GeneroModel>()
            .HasKey(g => g.IdGenero); // Define a chave primária de Genero
    }
}