using Microsoft.AspNetCore.Mvc;
using MockQueryable.NSubstitute;
using NSubstitute;
using Train.EfCore.SimpleAPI.Controllers;
using Train.EfCore.SimpleAPI.Data;
using Train.EfCore.SimpleAPI.Models;

namespace Train.EFCore.Tests.FakeDbSet;

public class FakeDbSetTest
{
    [Fact]
    public async Task IfGenreExists_ReturnGenres()
    {
        // Arrange
        var context = CreateFFakeDbContext();
        var controller = new GenresController(context);
        
        // Act
        var response = await controller.Get(2);
        var okResult = response as OkObjectResult;
        
        // Assert
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal("Action", (okResult.Value as Genre)?.Name);
        await context.DidNotReceive().SaveChangesAsync();
    }
    
    private MoviesContext CreateFFakeDbContext()
    {
        List<Genre> genres = new()
        {
            new Genre { Id = 1, Name = "Drama"},
            new Genre { Id = 2, Name = "Action"},
            new Genre { Id = 3, Name = "Comedy"}
        };
        
        var context = Substitute.For<MoviesContext>();
        var genreSet = genres.AsQueryable().BuildMockDbSet();

        genreSet.FindAsync(2)!.Returns(new ValueTask<Genre>(genres[1]));
        
        context.Genres.Returns(genreSet);

        return context;
    }
}