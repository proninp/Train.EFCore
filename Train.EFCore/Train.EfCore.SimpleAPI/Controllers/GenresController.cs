using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Train.EfCore.SimpleAPI.Data;
using Train.EfCore.SimpleAPI.Models;

namespace Train.EfCore.SimpleAPI.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Genres.ToListAsync());
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        
        return genre == null
            ? NotFound()
            : Ok(genre);
    }
}