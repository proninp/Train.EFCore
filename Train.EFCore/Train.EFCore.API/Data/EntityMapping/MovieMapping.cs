using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            .HasColumnType("date");

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
                Id = 1, Title = "The Silent River", ReleaseDate = new DateTime(2018, 3, 2),
                Synopsis = "A small town faces hidden truths after a flood.", MainGenreId = 1
            }, // Drama
            new Movie
            {
                Id = 2, Title = "Bytecode", ReleaseDate = new DateTime(2021, 11, 5),
                Synopsis = "A programmer uncovers a conspiracy inside an AI lab.", MainGenreId = 9
            }, // Science Fiction
            new Movie
            {
                Id = 3, Title = "Velvet Heist", ReleaseDate = new DateTime(2015, 6, 19),
                Synopsis = "An elaborate museum robbery tests loyalties.", MainGenreId = 12
            }, // Crime
            new Movie
            {
                Id = 4, Title = "Northern Lights", ReleaseDate = new DateTime(2013, 12, 6),
                Synopsis = "Two strangers meet during an aurora chase.", MainGenreId = 7
            }, // Romance
            new Movie
            {
                Id = 5, Title = "Iron Horizon", ReleaseDate = new DateTime(2019, 8, 23),
                Synopsis = "A special unit races to stop a rogue satellite.", MainGenreId = 3
            }, // Action
            new Movie
            {
                Id = 6, Title = "Paper Planes", ReleaseDate = new DateTime(2014, 4, 11),
                Synopsis = "A family rebuilds after a sudden move across the globe.", MainGenreId = 17
            }, // Family
            new Movie
            {
                Id = 7, Title = "Midnight Station", ReleaseDate = new DateTime(2020, 10, 9),
                Synopsis = "A detective hunts a killer haunting a night terminal.", MainGenreId = 5
            }, // Thriller
            new Movie
            {
                Id = 8, Title = "Echoes of War", ReleaseDate = new DateTime(2016, 5, 27),
                Synopsis = "Veterans return home to a divided village.", MainGenreId = 16
            }, // War
            new Movie
            {
                Id = 9, Title = "Arcadia", ReleaseDate = new DateTime(2017, 9, 15),
                Synopsis = "A cartographer discovers a hidden realm beyond the map.", MainGenreId = 8
            }, // Fantasy
            new Movie
            {
                Id = 10, Title = "The Last Frame", ReleaseDate = new DateTime(2022, 2, 18),
                Synopsis = "A documentarian unravels a lost reel’s mystery.", MainGenreId = 11
            } // Documentary
        );
    }
}