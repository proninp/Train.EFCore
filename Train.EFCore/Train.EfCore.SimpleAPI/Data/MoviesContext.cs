using Microsoft.EntityFrameworkCore;
using Train.EfCore.SimpleAPI.Data.EntityMapping;
using Train.EfCore.SimpleAPI.Models;

namespace Train.EfCore.SimpleAPI.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
    {
    }
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
    }
}