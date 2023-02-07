using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    public int DirectorId { get; set; }
    public bool Free { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
    [MaxLength(1024)]
    public string? FilmUrl { get; set; }

    //one director can have many films
    public virtual Director Director { get; set; } = null!;
    //one to many - one film can have many similar films and many genres
    public virtual ICollection<Genre> Genres { get; set; }
    public virtual ICollection<SimilarFilm> SimilarFilms { get; set; }
}
