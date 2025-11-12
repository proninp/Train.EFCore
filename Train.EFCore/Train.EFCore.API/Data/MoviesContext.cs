using Microsoft.EntityFrameworkCore;
using Train.EFCore.API.Data.EntityMapping;
using Train.EFCore.API.Models;

namespace Train.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("""
                                    Data Source=localhost,1435;
                                    Initial Catalog=MoviesDB;
                                    User ID=sa;
                                    Password=MyPassworth123;
                                    TrustServerCertificate=True;
                                    """);
        // Not proper logging
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieMapping).Assembly);
    }
}