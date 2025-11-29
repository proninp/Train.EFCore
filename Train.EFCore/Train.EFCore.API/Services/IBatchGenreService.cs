using Train.EFCore.API.Models;

namespace Train.EFCore.API.Services;

public interface IBatchGenreService
{
    Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres);
}