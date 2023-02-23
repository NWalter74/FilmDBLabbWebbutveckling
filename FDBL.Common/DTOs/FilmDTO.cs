namespace FDBL.Common.DTOs;

//In a DTO we only have data we want the user to see

public class FilmDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime Released { get; set; }
    public bool Free { get; set; }
    public string? Description { get; set; }
    public string? FilmUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageLargeUrl { get; set; }
    public string? ImageSimilarUrl { get; set; }

    public int DirectorId { get; set; }
    public virtual DirectorDTO? Director { get; set; }      //DirectorDTO instead of an Director entity

    public virtual List<GenreDTO>? Genres { get; set; } = new();
    public virtual List<SimilarFilmDTO>? SimilarFilms { get; set; } = new();

}

public class FilmCreateDTO
{
    public string? Title { get; set; }
    public DateTime Released { get; set; } = DateTime.Today;
    public int DirectorId { get; set; }
    public bool Free { get; set; }
    public string? Description { get; set; }
    public string? FilmUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageLargeUrl { get; set; }
    public string? ImageSimilarUrl { get; set; }
}

public class FilmEditDTO : FilmCreateDTO
{
    public int Id { get; set; }
}
