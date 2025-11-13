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

        builder
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasPrincipalKey(g => g.Id)
            .HasForeignKey(m => m.MainGenreId);

        builder.HasData(
            new Movie
            {
                Id = 1,
                Title = "Shadows of Tomorrow",
                ReleaseDate = new DateTime(2019, 5, 17),
                Synopsis = "A scientist struggles with the consequences of a failed time experiment.",
                AgeRating = AgeRating.Adolescent,
                MainGenreId = 9 // Science Fiction
            },
            new Movie
            {
                Id = 2,
                Title = "Laughing in the Rain",
                ReleaseDate = new DateTime(2014, 3, 8),
                Synopsis = "A clumsy waiter turns a rainy week into the funniest days of his life.",
                AgeRating = AgeRating.ElementarySchool,
                MainGenreId = 2 // Comedy
            },
            new Movie
            {
                Id = 3,
                Title = "Broken Silence",
                ReleaseDate = new DateTime(2007, 11, 2),
                Synopsis = "A family drama about unspoken truths and reconciliation after years apart.",
                AgeRating = AgeRating.HighSchool,
                MainGenreId = 1 // Drama
            },
            new Movie
            {
                Id = 4,
                Title = "Edge of the City",
                ReleaseDate = new DateTime(2021, 9, 24),
                Synopsis = "An undercover cop infiltrates a dangerous crime syndicate at the edge of a megacity.",
                AgeRating = AgeRating.Adult,
                MainGenreId = 12 // Crime
            },
            new Movie
            {
                Id = 5,
                Title = "Hidden Trail",
                ReleaseDate = new DateTime(2016, 7, 15),
                Synopsis = "Two friends embark on a risky mountain hike that turns into a fight for survival.",
                AgeRating = AgeRating.Adolescent,
                MainGenreId = 4 // Adventure
            },
            new Movie
            {
                Id = 6,
                Title = "Night Whispers",
                ReleaseDate = new DateTime(2018, 10, 31),
                Synopsis = "A small town is haunted by eerie voices only heard after midnight.",
                AgeRating = AgeRating.Adolescent,
                MainGenreId = 6 // Horror
            },
            new Movie
            {
                Id = 7,
                Title = "Hearts Between Pages",
                ReleaseDate = new DateTime(2013, 2, 14),
                Synopsis = "A shy librarian and a struggling writer find each other through a lost notebook.",
                AgeRating = AgeRating.HighSchool,
                MainGenreId = 7 // Romance
            },
            new Movie
            {
                Id = 8,
                Title = "Kingdom of Emberlight",
                ReleaseDate = new DateTime(2020, 12, 18),
                Synopsis = "A young mage must protect a kingdom powered by ancient light crystals.",
                AgeRating = AgeRating.ElementarySchool,
                MainGenreId = 8 // Fantasy
            },
            new Movie
            {
                Id = 9,
                Title = "Lines of Truth",
                ReleaseDate = new DateTime(2011, 4, 29),
                Synopsis = "A detective with a photographic memory investigates a tangled web of lies.",
                AgeRating = AgeRating.Adolescent,
                MainGenreId = 13 // Mystery
            },
            new Movie
            {
                Id = 10,
                Title = "Echoes of the Front",
                ReleaseDate = new DateTime(2004, 5, 9),
                Synopsis = "Soldiers on the front line struggle with loyalty, fear, and hope during a brutal war.",
                AgeRating = AgeRating.Adult,
                MainGenreId = 16 // War
            },
            new Movie
            {
                Id = 11,
                Title = "Racing the Finish Line",
                ReleaseDate = new DateTime(2017, 6, 3),
                Synopsis = "A young runner from a small town fights for a chance at the national championship.",
                AgeRating = AgeRating.ElementarySchool,
                MainGenreId = 18 // Sport
            },
            new Movie
            {
                Id = 12,
                Title = "Worlds in Motion",
                ReleaseDate = new DateTime(2022, 1, 21),
                Synopsis = "A documentary about everyday people changing their communities in unexpected ways.",
                AgeRating = AgeRating.All,
                MainGenreId = 11 // Documentary
            }
        );
    }
}