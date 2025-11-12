using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train.EFCore.API.Models;

namespace Train.EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.HasData(
            new Genre { Id = 1, Name = "Drama" },
            new Genre { Id = 2, Name = "Comedy" },
            new Genre { Id = 3, Name = "Action" },
            new Genre { Id = 4, Name = "Adventure" },
            new Genre { Id = 5, Name = "Thriller" },
            new Genre { Id = 6, Name = "Horror" },
            new Genre { Id = 7, Name = "Romance" },
            new Genre { Id = 8, Name = "Fantasy" },
            new Genre { Id = 9, Name = "Science Fiction" },
            new Genre { Id = 10, Name = "Animation" },
            new Genre { Id = 11, Name = "Documentary" },
            new Genre { Id = 12, Name = "Crime" },
            new Genre { Id = 13, Name = "Mystery" },
            new Genre { Id = 14, Name = "Historical" },
            new Genre { Id = 15, Name = "Musical" },
            new Genre { Id = 16, Name = "War" },
            new Genre { Id = 17, Name = "Family" },
            new Genre { Id = 18, Name = "Sport" }
        );
    }
}