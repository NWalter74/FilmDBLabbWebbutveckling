namespace FDBL.Membership.Database.Entities;

public class Film : IEntity
{
    public Film()
    {
        SimilarFilms = new HashSet<SimilarFilm>();
        Genres = new HashSet<Genre>();
    }

    public int Id { get; set; }
    [MaxLength(50)]
    public string? Title { get; set; }
    public DateTime Released { get; set; }
    public bool Free { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [MaxLength(1024)]
    public string? FilmUrl { get; set; }
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    [MaxLength(255)]
    public string? ImageLargeUrl { get; set; }
    [MaxLength(255)]
    public string? ImageSimilarUrl { get; set; }

    //Puts tables together. (Relation as in the diagram)
    public int DirectorId { get; set; }

    //one director can have many films
    //With the Director property I can load related director data when fetching the film.
    //(Navigationproperty: When I get a film I even want to get data for the director at the same time.)
    public virtual Director? Director { get; set; }

    //one to many - one film can have many similar films and many genres
    /*You can use the navigation propertries to eager-load related data by including the tables when fetching the data, or to simplify LINQ queries by avoiding complex joins. */
    public virtual ICollection<Genre>? Genres { get; set; }      
    public virtual ICollection<SimilarFilm>? SimilarFilms { get; set; }  
}
