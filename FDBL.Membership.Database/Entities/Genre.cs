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
    /*You can use the navigation propertries to eager-load related data by including the tables when fetching the data, or to simplify LINQ queries by avoiding complex joins. */
    public virtual ICollection<Film>? Films { get; set; } 
}
