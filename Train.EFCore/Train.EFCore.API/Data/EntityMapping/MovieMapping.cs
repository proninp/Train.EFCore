using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Train.EFCore.API.Data.ValueConverters;
using Train.EFCore.API.Models;

namespace Train.EFCore.API.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            //.HasQueryFilter(m => m.ReleaseDate >= new DateTime(2000, 1, 1))
            .HasKey(m => m.Id);

        builder
            .Property(m => m.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(m => m.ReleaseDate)
            .HasColumnType("char(8)")
            .HasConversion(new DateTimeToChar8Converter());

        builder
            .Property(m => m.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");

        //builder.ComplexProperty(m => m.Director);
        builder.OwnsOne(m => m.Director)
            .ToTable("Movie_Directors");

        builder.OwnsMany(m => m.Actors)
            .ToTable("Movie_Actors");

        builder
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasPrincipalKey(g => g.Id)
            .HasForeignKey(m => m.MainGenreId);

        builder.HasData(
            new Movie
            {
                Id = 1,
                Title = "Fight Club",
                ReleaseDate = new DateTime(1999, 10, 15),
                Synopsis = "An insomniac office worker and a soap maker form an underground fight club.",
                AgeRating = AgeRating.Adult,
                MainGenreId = 12 // Crime (ближе всего по смыслу)
            }
        );
        
        builder.OwnsOne(m => m.Director).HasData(
            new
            {
                MovieId = 1,
                FirstName = "David",
                LastName = "Fincher"
            }
        );
        
        builder.OwnsMany(m => m.Actors).HasData(
            // Fight Club (MovieId = 1)
            new
            {
                Id = 1,
                MovieId = 1,
                FirstName = "Brad",
                LastName = "Pitt"
            },
            new
            {
                Id = 2,
                MovieId = 1,
                FirstName = "Edward",
                LastName = "Norton"
            },
            new
            {
                Id = 3,
                MovieId = 1,
                FirstName = "Helena",
                LastName = "Bonham Carter"
            }
        );
    }
}