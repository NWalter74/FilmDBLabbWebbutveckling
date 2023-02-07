namespace FDBL.Common.DTOs;

public class FilmGenreDTO
{
    public int FilmId { get; set; } = default;
    public int GenreId { get; set; } = default;

    public FilmGenreDTO(int filmId, int genreId) => (FilmId, GenreId) = (filmId, genreId);

}
