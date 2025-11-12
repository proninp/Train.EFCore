using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train.EFCore.API.Data;
using Train.EFCore.API.Models;

namespace Train.EFCore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : Controller
{
    private readonly MoviesContext _context;

    public GenresController(MoviesContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _context.Genres.ToListAsync(ct));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct)
    {
        var genre = await _context.FindAsync<Genre>(id, ct);
        
        return genre is null
            ? NotFound()
            : Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Genre genre, CancellationToken ct)
    {
        await _context.Genres.AddAsync(genre, ct);
        await _context.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Genre genre, CancellationToken ct)
    {
        var existingGenre = await _context.Genres.FindAsync(id, ct);
        if (existingGenre is null)
        {
            return NotFound();
        }
        
        existingGenre.Name = genre.Name;
        await _context.SaveChangesAsync(ct);
        return Ok(existingGenre);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
    {
        var existingGenre = await _context.Genres.FindAsync(id, ct);
        if (existingGenre is null)
        {
            return NotFound();
        }
        
        _context.Genres.Remove(existingGenre);
        await _context.SaveChangesAsync(ct);
        return Ok();
    }
}