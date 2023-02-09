namespace FDBL.Common.DTOs;

public class SimilarFilmDTO
{
    public int FilmId { get; set; } = default; 
    public int SimilarFilmId { get; set; } = default;

    public FilmDTO? Film { get; set; }
    public FilmDTO? Similar { get; set; }

    //public SimilarFilmDTO(int filmId, int similarFilmId) => (FilmId, SimilarFilmId) = (filmId, similarFilmId);
}
