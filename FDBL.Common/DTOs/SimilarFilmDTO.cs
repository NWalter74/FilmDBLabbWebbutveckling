namespace FDBL.Common.DTOs;

//In a DTO we only have data we want the user to see

public class SimilarFilmDTO
{
    public int FilmId { get; set; } = default; 
    public int SimilarFilmId { get; set; } = default;

    public FilmDTO? Film { get; set; }
    public FilmDTO? Similar { get; set; }
}
