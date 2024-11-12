using LivrariaOnlineAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaOnlineAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
    
    public DbSet<LivroModel> Livros { get; set; }
}