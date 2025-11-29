using Microsoft.EntityFrameworkCore;
using Train.EFCore.API.Data.EntityMapping;
using Train.EFCore.API.Models;

namespace Train.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
    {
        
    }
    
    public DbSet<Movie> Movies => Set<Movie>();
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieMapping).Assembly);
    }
}