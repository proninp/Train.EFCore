using Microsoft.EntityFrameworkCore;
using Train.EFCore.API.Data;
using Train.EFCore.API.Models;

namespace Train.EFCore.API.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MoviesContext _moviesContext;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public GenreRepository(MoviesContext moviesContext, IUnitOfWorkManager unitOfWorkManager)
    {
        _moviesContext = moviesContext;
        _unitOfWorkManager = unitOfWorkManager;
    }
    
    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _moviesContext.Genres.ToListAsync();
    }

    public async Task<Genre?> Get(int id)
    {
        return await _moviesContext.Genres.FindAsync(id);
    }

    public async Task<Genre> Create(Genre genre)
    {
        await _moviesContext.Genres.AddAsync(genre);
        
        if (!_unitOfWorkManager.IsUnitOfWorkStarted)
            await _moviesContext.SaveChangesAsync();
        
        return genre;
    }

    public async Task<Genre?> Update(int id, Genre genre)
    {
        var existingGenre = await _moviesContext.Genres.FindAsync(id);
        
        if (existingGenre is null)
            return null;
        
        existingGenre.Name = genre.Name;
        if (!_unitOfWorkManager.IsUnitOfWorkStarted)
            await _moviesContext.SaveChangesAsync();
        
        return existingGenre;
    }

    public async Task<bool> Delete(int id)
    {
        var existingGenre = await _moviesContext.Genres.FindAsync(id);
        
        if (existingGenre is null)
            return false;

        _moviesContext.Genres.Remove(existingGenre);
            
        if (!_unitOfWorkManager.IsUnitOfWorkStarted)
            await _moviesContext.SaveChangesAsync();
        
        return true;
    }
}