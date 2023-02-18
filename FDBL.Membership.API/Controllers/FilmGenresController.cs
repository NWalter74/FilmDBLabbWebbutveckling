using FDBL.Membership.API.Utilites;

namespace FDBL.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmGenresController : ControllerBase
{
    private readonly IDbService _db;



    public FilmGenresController(IDbService db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            _db.Include<FilmGenre>();
            List<FilmGenreDTO>? filmGenres = await _db.GetAsync<FilmGenre, FilmGenreDTO>();
            return Results.Ok(JsonUtility.RemoveLoops(filmGenres));
        }
        catch
        {
            return Results.NotFound();
        }
    }
    [HttpPut("{id}")]

    public async Task<IResult> Put(int id, [FromBody] List<FilmGenreDTO> dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest();
            }
            var toKeep = await _db.GetAsync<FilmGenre, FilmGenreDTO>(a => a.FilmId == id && dto.Select(b => b.GenreId).ToList().Contains(a.GenreId));
            var toDelete = await _db.GetAsync<FilmGenre, FilmGenreDTO>(a => a.FilmId == id && !dto.Select(a => a.GenreId).ToList().Contains(a.GenreId));
            var toAdd = dto.Where(a => !toKeep.Select(b => b.GenreId).ToList().Contains(a.GenreId)).ToList();

            foreach (var item in toDelete)
            {
                _db.DeleteAsync<FilmGenre>(new FilmGenre() { FilmId = (int)item.FilmId, GenreId = (int)item.GenreId });
                await _db.SaveChangesAsync();
            }
            foreach (var item in toAdd)
            {
                _db.AddAsync<FilmGenre, FilmGenreDTO>(item);
            }

            var success = await _db.SaveChangesAsync();
            if (!success)
            {
                return Results.BadRequest();
            }
            return Results.NoContent();
        }
        catch
        {
            return Results.BadRequest();
        }
    }
}
