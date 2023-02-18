namespace FDBL.Common.Services;

public interface IMembershipService
{
    Task<FilmDTO> GetFilmAsync(int? id);
    Task<List<FilmDTO>> GetFilmsAsync();
    Task<GenreDTO> GetGenreAsync(int? id);
    Task<List<GenreDTO>> GetGenresAsync();
    Task<FilmGenreDTO> GetFilmGenreAsync(int? id);
    Task<List<FilmGenreDTO>> GetFilmGenresAsync();
    Task<SimilarFilmDTO> GetSimilarFilmAsync(int? id);
    Task<List<SimilarFilmDTO>> GetSimilarFilmsAsync();
}