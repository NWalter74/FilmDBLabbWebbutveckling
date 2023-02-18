namespace FDBL.Users.Database.Contexts;

public class FDBLUserContext : IdentityDbContext<FDBLUser>
{
    public FDBLUserContext(DbContextOptions<FDBLUserContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
