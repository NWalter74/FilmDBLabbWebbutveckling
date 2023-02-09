namespace FDBL.Membership.Database.Entities;

public class Director : IEntity
{
    public Director()
    {
        Films = new HashSet<Film>();
    }

    public int Id { get; set; }
    [MaxLength(50)]
    public string? Name { get; set; }

    //one to many - one director can have many films
    public virtual ICollection<Film>? Films { get; set; }   
}
