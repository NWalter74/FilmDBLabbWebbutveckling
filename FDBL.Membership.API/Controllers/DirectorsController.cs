namespace FDBL.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectorsController : ControllerBase
{
    private readonly IDbService _db;

    public DirectorsController(IDbService db) => _db = db;

    // GET: api/<DirectorsController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            List<DirectorDTO>? directors = await _db.GetAsync<Director, DirectorDTO>();

            return Results.Ok(directors);
        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // GET api/<DirectorsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            var director =
                await _db.SingleAsync<Director, DirectorDTO>(c => c.Id.Equals(id));

            return Results.Ok(director);

        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // POST api/<DirectorsController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] DirectorDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();

            var director = await _db.AddAsync<Director, DirectorDTO>(dto);

            var success = await _db.SaveChangesAsync();

            if (!success)
            {
                return Results.BadRequest();
            }

            return Results.Created(_db.GetURI<Director>(director), director);
        }
        catch
        {
        }

        return Results.BadRequest();
    }

    // PUT api/<DirectorsController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] DirectorDTO dto)
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

            var exists = await _db.AnyAsync<Director>(c => c.Id.Equals(id));
            if (!exists)
            {
                return Results.NotFound("Could not find entity");
            }
            _db.Update<Director, DirectorDTO>(dto.Id, dto);

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


    // DELETE api/<DirectorsController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            var success = await _db.DeleteAsync<Director>(id);

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
