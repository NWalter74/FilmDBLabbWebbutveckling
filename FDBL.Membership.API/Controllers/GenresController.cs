using FDBL.Membership.Database.Entities;

namespace FDBL.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IDbService _db;

    public GenresController(IDbService db) => _db = db;

    // GET: api/<GenresController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            List<GenreDTO>? genres = await _db.GetAsync<Genre, GenreDTO>();

            return Results.Ok(genres);
        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // GET api/<GenresController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            var genre =
                await _db.SingleAsync<Genre, GenreDTO>(c => c.Id.Equals(id));

            return Results.Ok(genre);

        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // POST api/<GenresController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] GenreDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();

            var genre = await _db.AddAsync<Genre, GenreDTO>(dto);

            var success = await _db.SaveChangesAsync();

            if (!success)
            {
                return Results.BadRequest();
            }

            return Results.Created(_db.GetURI<Genre>(genre), genre);
        }
        catch
        {
        }

        return Results.BadRequest();
    }

    // PUT api/<GenresController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] GenreDTO dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("No entity provided");
            }
            if (!id.Equals(dto.Id))
            {
                return Results.BadRequest("Differing ids");
            }

            var exists = await _db.AnyAsync<Genre>(c => c.Id.Equals(id));
            if (!exists)
            {
                return Results.NotFound("Could not find entity");
            }
            _db.Update<Genre, GenreDTO>(dto.Id, dto);

            var success = await _db.SaveChangesAsync();

            if (!success)
            {
                return Results.BadRequest();
            }

            return Results.NoContent();
        }
        catch
        {
        }

        return Results.BadRequest("Unable to update the entity");
    }


    // DELETE api/<GenresController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            var success = await _db.DeleteAsync<Genre>(id);

            if (!success)
            {
                return Results.NotFound();
            }

            success = await _db.SaveChangesAsync();

            if (!success)
            {
                return Results.BadRequest();
            }

            return Results.NoContent();
        }
        catch
        {
        }

        return Results.BadRequest();
    }
}
