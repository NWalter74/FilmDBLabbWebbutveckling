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

    //TODO:Only workaround! Check for better way if time left att the end. Check even for datetime format
    public string ReleasedString
    {
        get
        {
            return Released.ToString();
        }
        set
        {
            Released = DateTime.Parse(value);
        }
    }

    public int DirectorId { get; set; }
    public virtual DirectorDTO? Director { get; set; }      //DirectorDTO instead of an Director entity

    public virtual List<GenreDTO>? Genres { get; set; } = new();
    public virtual List<SimilarFilmDTO>? SimilarFilms { get; set; } = new();

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
