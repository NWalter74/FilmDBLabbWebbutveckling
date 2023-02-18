using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FDBL.Common.DTOs;

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

    //TODO:Ask Koosha: Why did we this here? Why video? 
    public string GetVideoId()
    {
        try
        {
            return HttpUtility.ParseQueryString(new Uri(FilmUrl).Query).Get("v");
        }
        catch
        {
            //TODO: Why return null? 
            return null;
        }
    }
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
