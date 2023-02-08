namespace FDBL.Membership.Database.Entities;

public class FilmGenre : IReferenceEntity
{
    public int FilmId { get; set; }     
    public int GenreId { get; set; }    

    //one film can have many filmgenres
    public virtual Film? Film { get; set; }
    //one genre can have many filmgenres
    public virtual Genre? Genres { get; set; }
}