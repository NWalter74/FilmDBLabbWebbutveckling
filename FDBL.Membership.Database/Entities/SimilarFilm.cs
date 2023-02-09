using System.ComponentModel.DataAnnotations.Schema;

namespace FDBL.Membership.Database.Entities;

public class SimilarFilm //: IReferenceEntity
{
    public int FilmId { get; set; }         
    public int SimilarFilmId { get; set; }  

    //one film can have many parentfilms and many similar films
    public virtual Film Film { get; set; } = null!;
    [ForeignKey("SimilarFilmId")]
    public virtual Film Similar { get; set; } = null!;
}
