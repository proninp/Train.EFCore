using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Train.EFCore.API.Models;

public class Movie
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public string? Synopsis { get; set; }
    
    public AgeRating AgeRating { get; set; }
    
    public decimal InternetRating { get; set; }
    
    public Person? Director { get; set; }

    public ICollection<Person> Actors { get; set; } = [];
    
    public int MainGenreId { get; set; }
    
    public Genre Genre { get; set; }
}