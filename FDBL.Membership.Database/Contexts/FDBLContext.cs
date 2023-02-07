using Microsoft.EntityFrameworkCore;

namespace FDBL.Membership.Database.Contexts;

public class FDBLContext : DbContext
{
    //Här definieras namnen för tabellerna i databasen och "tilldelas" rätt klass i VS
    public virtual DbSet<Director> Directors => Set<Director>();
    public virtual DbSet<Film> Films => Set<Film>();
    public virtual DbSet<FilmGenre> FilmGenres => Set<FilmGenre>();
    public virtual DbSet<Genre> Genres => Set<Genre>();
    public virtual DbSet<SimilarFilm> SimilarFilms => Set<SimilarFilm>();

    public FDBLContext(DbContextOptions<FDBLContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FilmGenre>().HasKey(ci => new { ci.FilmId, ci.GenreId });

        builder.Entity<SimilarFilm>().HasKey(ci => new { ci.FilmId, ci.SimilarFilmId });
        
        base.OnModelCreating(builder);

        /* Configuring related tables for the Film table*/
        builder.Entity<Film>(entity =>
        {
            entity
                // For each film in the Film Entity,
                // reference relatred films in the SimilarFilms entity
                // with the ICollection<SimilarFilms>
                .HasMany(d => d.SimilarFilms)
                .WithOne(p => p.Film)
                .HasForeignKey(d => d.FilmId)
                // To prevent cycles or multiple cascade paths.
                // Neded when seeding similar films data.
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure a many-to-many realtionship between genres
            // and films using the FilmGenre connection entity.
            entity.HasMany(d => d.Genres)
                  .WithMany(p => p.Films)
                  .UsingEntity<FilmGenre>()
                  // Specify the table name for the connection table
                  // to avoid duplicate tables (FilmGenre and FilmGenres)
                  // in the database.
                  .ToTable("FilmGenres");
        });

        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var directors = new List<Director>
        {
            new Director { Id = 1001, Name = "Brad Peyton" },
            new Director { Id = 1002, Name = "Jordan Vogt-Roberts" },
            new Director { Id = 1003, Name = "Brad Peyton" }
        };

        var films = new List<Film>
        {
            new Film
            {
                Id = 11,
                Title = "Rampage",
                Released = new DateTime(2018, 04, 13),
                DirectorId = 1001,
                Free = false,
                Description = "När tre olika djur smittas av en farlig patogen...",
                FilmUrl = "https://youtu.be/coOKvrsmQiI"
            },
            new Film
            {
                Id = 12,
                Title = "San Andreas",
                Released = new DateTime(2015, 05, 29),
                DirectorId = 1001,
                Free = false,
                Description = "När den okända San Andreas förkastningen slutligen ger vika...",
                FilmUrl = "https://youtu.be/23VflsU3kZE"
            },
            new Film
            {
                Id = 13,
                Title = "Kong Skull Island",
                Released = new DateTime(2017, 03, 10),
                DirectorId = 1002,
                Free = false,
                Description = "En brokig skara forskare, soldater och äventyrare samlas för att utforska...",
                FilmUrl = "https://youtu.be/dBLdPIp-BuY"

            },
            new Film
            {
                Id = 14,
                Title = "Journey2",
                Released = new DateTime(2012, 03, 02),
                DirectorId = 1001,
                Free = false,
                Description = "Den unge Sean Anderson får ett kodat nödanrop från...",
                FilmUrl = "https://youtu.be/1Q2LVXlHKS8"

            },
            new Film
            {
                Id = 15,
                Title = "Arthur The War Of Two Worlds",
                Released = new DateTime(2011, 04, 20),
                DirectorId = 1003,
                Free = false,
                Description = "Maltazard har fångat Arthur i Minimojernas rike och har påbörjat...",
                FilmUrl = "https://youtu.be/QWHsbDbLCJM"
            }
        };

        var similarFilms = new List<SimilarFilm>
        {
            new SimilarFilm { FilmId = 11, SimilarFilmId = 12 },
            new SimilarFilm { FilmId = 11, SimilarFilmId = 13 },
            new SimilarFilm { FilmId = 11, SimilarFilmId = 14 },
            new SimilarFilm { FilmId = 12, SimilarFilmId = 11 },
            new SimilarFilm { FilmId = 12, SimilarFilmId = 14 },
            new SimilarFilm { FilmId = 12, SimilarFilmId = 13 },
            new SimilarFilm { FilmId = 13, SimilarFilmId = 11 },
            new SimilarFilm { FilmId = 13, SimilarFilmId = 15 },
            new SimilarFilm { FilmId = 13, SimilarFilmId = 14 },
            new SimilarFilm { FilmId = 14, SimilarFilmId = 12 },
            new SimilarFilm { FilmId = 14, SimilarFilmId = 11 },
            new SimilarFilm { FilmId = 14, SimilarFilmId = 15 },
            new SimilarFilm { FilmId = 15, SimilarFilmId = 12 },
            new SimilarFilm { FilmId = 15, SimilarFilmId = 13 },
            new SimilarFilm { FilmId = 15, SimilarFilmId = 14 }
        };

        var genres = new List<Genre>
        {
            new Genre { Id = 101, Name = "Action" },
            new Genre { Id = 102, Name = "Sci-Fi" },
            new Genre { Id = 103, Name = "Adventure" },
            new Genre { Id = 104, Name = "Thriller" },
            new Genre { Id = 105, Name = "Fantasy" },
            new Genre { Id = 106, Name = "Comedy" },
            new Genre { Id = 107, Name = "Animation" },
            new Genre { Id = 108, Name = "Family" }
        };

        var filmGenres = new List<FilmGenre>
        {
            new FilmGenre { FilmId = 11, GenreId = 101 },
            new FilmGenre { FilmId = 11, GenreId = 102 },
            new FilmGenre { FilmId = 11, GenreId = 103 },
            new FilmGenre { FilmId = 12, GenreId = 101 },
            new FilmGenre { FilmId = 12, GenreId = 103 },
            new FilmGenre { FilmId = 12, GenreId = 104 },
            new FilmGenre { FilmId = 13, GenreId = 101 },
            new FilmGenre { FilmId = 13, GenreId = 103 },
            new FilmGenre { FilmId = 13, GenreId = 105 },
            new FilmGenre { FilmId = 14, GenreId = 101 },
            new FilmGenre { FilmId = 14, GenreId = 103 },
            new FilmGenre { FilmId = 14, GenreId = 106 },
            new FilmGenre { FilmId = 15, GenreId = 103 },
            new FilmGenre { FilmId = 15, GenreId = 105 },
            new FilmGenre { FilmId = 15, GenreId = 106 }
        };

        builder.Entity<Director>().HasData(directors);
        builder.Entity<Film>().HasData(films);
        builder.Entity<SimilarFilm>().HasData(similarFilms);
        builder.Entity<Genre>().HasData(genres);
        builder.Entity<FilmGenre>().HasData(filmGenres);
    }
}