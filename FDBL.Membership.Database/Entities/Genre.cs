namespace FDBL.Membership.Database.Entities;

public class Genre : IEntity
{
    public Genre()
    {
        Films = new HashSet<Film>();
    }

    public int Id { get; set; }

    [MaxLength(50)]
    public string? Name { get; set; }

    //one to many - one genre can have many films
    public virtual ICollection<Film>? Films { get; set; } 
}
