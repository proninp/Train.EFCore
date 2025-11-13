using Train.EFCore.API.Data;
using Train.EFCore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Train.EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    private readonly MoviesContext _context;
    public MoviesController(MoviesContext context)
    {
        _context = context;
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Movies.ToListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct)
    {
        var movie = await _context.Movies
            .Include(m => m.Genre)
            .SingleOrDefaultAsync(m => m.Id == id, ct);
        
        return movie is null
            ? NotFound()
            : Ok(movie);
    }

    [HttpGet("by-year/{year:int}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByYear([FromRoute] int year, CancellationToken ct)
    {
        var filteredMovies = await _context.Movies
            .Where(m => m.ReleaseDate.Year == year)
            .Select(m => new MovieTitle { Id = m.Id, Title = m.Title })
            .ToListAsync(cancellationToken: ct);
        return Ok(filteredMovies);
    }
    
    [HttpGet("until-age/{ageRating}")]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUntilAge([FromRoute] AgeRating ageRating)
    {
        var filteredTitles = await _context.Movies
            .Where(m => m.AgeRating <= ageRating)
            .Select(m => new MovieTitle { Id = m.Id, Title = m.Title })
            .ToListAsync();
        return Ok(filteredTitles);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie,  CancellationToken ct)
    {
        await _context.Movies.AddAsync(movie, ct);
        await _context.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetAll), new { id = movie.Id }, movie);
    }
    
    [HttpPost("list")]
    [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] IEnumerable<Movie> movies,  CancellationToken ct)
    {
        foreach (var movie in movies)
        {
            await _context.Movies.AddAsync(movie, ct);
        }
        await _context.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetAll), null, movies);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie,  CancellationToken ct)
    {
        var existingMovie = await _context.Movies.FindAsync(id, ct);

        if (existingMovie is null)
        {
            return NotFound();
        }
        
        existingMovie.Title = movie.Title;
        existingMovie.ReleaseDate = movie.ReleaseDate;
        existingMovie.Synopsis = movie.Synopsis;
        
        await _context.SaveChangesAsync(ct);
        return Ok(existingMovie);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
    {
        var existingMovie = await _context.Movies.FindAsync(id, ct);
        if (existingMovie is null)
        {
            return NotFound();
        }
        
        _context.Movies.Remove(existingMovie);
        await _context.SaveChangesAsync(ct);
        
        return Ok();
    }
}