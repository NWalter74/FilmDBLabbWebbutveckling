namespace FDBL.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SimilarFilmsController : ControllerBase
{
    private readonly IDbService _db;



    public SimilarFilmsController(IDbService db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            _db.Include<SimilarFilm>();
            List<SimilarFilmDTO>? similarFilms = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>();
            return Results.Ok(JsonUtility.RemoveLoops(similarFilms));
        }
        catch
        {
            return Results.NotFound();
        }
    }
    [HttpPut("{id}")]

    public async Task<IResult> Put(int id, [FromBody] List<SimilarFilmDTO> dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest();
            }
            var toKeep = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>(a => a.FilmId == id && dto.Select(b => b.SimilarFilmId).ToList().Contains(a.SimilarFilmId));
            var toDelete = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>(a => a.FilmId == id && !dto.Select(a => a.SimilarFilmId).ToList().Contains(a.SimilarFilmId));
            var toAdd = dto.Where(a => !toKeep.Select(b => b.SimilarFilmId).ToList().Contains(a.SimilarFilmId)).ToList();

            foreach (var item in toDelete)
            {
                _db.DeleteAsync<SimilarFilm>(new SimilarFilm() { FilmId = (int)item.FilmId, SimilarFilmId = (int)item.SimilarFilmId });
                await _db.SaveChangesAsync();
            }
            foreach (var item in toAdd)
            {
                _db.AddAsync<SimilarFilm, SimilarFilmDTO>(item);
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
