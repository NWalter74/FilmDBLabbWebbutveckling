﻿namespace FDBL.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmsController : ControllerBase
{
    private readonly IDbService _db;

    public FilmsController(IDbService db) => _db = db;

    // GET: api/<FilmsController>
    [HttpGet]
    public async Task<IResult> Get(bool freeOnly)
    {
        try
        {
            _db.Include<Director>();

            List<FilmDTO>? films = freeOnly ?
                await _db.GetAsync<Film, FilmDTO>(c => c.Free.Equals(freeOnly)) :
                await _db.GetAsync<Film, FilmDTO>();

            return Results.Ok(films);
        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // GET api/<FilmsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            _db.Include<Director>();
            _db.Include<Genre>();

            var film =
                await _db.SingleAsync<Film, FilmDTO>(c => c.Id.Equals(id));

            return Results.Ok(film);

        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // POST api/<FilmsController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();

            var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);

            var success = await _db.SaveChangesAsync();

            if (!success)
            {
                return Results.BadRequest();
            }

            return Results.Created(_db.GetURI<Film>(film), film);
        }
        catch
        {
        }

        return Results.BadRequest();
    }

    // PUT api/<FilmsController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
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

            var exists = await _db.AnyAsync<Director>(i => i.Id.Equals(dto.DirectorId));
            if (!exists)
            {
                return Results.NotFound("Could not find related entity");
            }

            exists = await _db.AnyAsync<Film>(c => c.Id.Equals(id));
            if (!exists)
            {
                return Results.NotFound("Could not find entity");
            }
            _db.Update<Film, FilmEditDTO>(dto.Id, dto);

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


    // DELETE api/<FilmsController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            //Inside the try block, call the DeleteAsync method on the _db service instance and specify the 
            //Course entity as its type.Save there result in a variable named success.
            var success = await _db.DeleteAsync<Film>(id);

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