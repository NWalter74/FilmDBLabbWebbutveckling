namespace FDBL.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmsController : ControllerBase
{
    //Inject the IDbService service and store it in a class-level variable named _db.
    private readonly IDbService _db;

    public FilmsController(IDbService db) => _db = db;

    // GET: api/<FilmsController>
    /*The GetAsync method in the DbService returns all films asynchronously when called from the FilmsController’s HttpGet action method. You fetch data from the Films table and receive it as 
     *FilmDTO objects by specifying the Film entity and the FilmDTO when calling the GetAsync method.
     *The HttpGet action method returns the fetched data as an asynchronous Task<IResult>. Using a task frees up resources for other tasks while fetching the data.*/
    [HttpGet]
    public async Task<IResult> Get(bool freeOnly)
    {
        try
        {
            // call the Include method on the _db service instance and specify the Director entity as its types to include director data when fetching the films
            _db.Include<Director>();

            //Await the result from a call to the GetAsync method on the _db service instance and specify the Film entity and FilmDTO as its types. Store the result in a variable named films.
            List<FilmDTO>? films = freeOnly ?
                await _db.GetAsync<Film, FilmDTO>(c => c.Free.Equals(freeOnly)) :
                await _db.GetAsync<Film, FilmDTO>();

            //Return the result in the films variable wrapped in a call to the Results.Ok response status method.
            return Results.Ok(films);
        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // GET api/<FilmsController>/5
    /*The SingleAsync method in the DbService returns one course asynchronously when called from the FilmsController’s HttpGet action method. You fetch data from the Films table and receive it as 
     *a FilmDTO object by specifying the Film entity and the FilmDTO when calling the SingleAsync method.
     *The HttpGet action method returns the fetched data as an asynchronous Task<IResult>. Using a task frees up resources for other tasks while fetching the data.*/
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            // call the Include method on the _db service instance to include data when fetching the film
            _db.Include<Director>();
            _db.Include<Genre>();

            //Await the result from a call to the SingleAsync method on the _db service instance and specify the Film entity and FilmDTO as its types.Store the result in a variable named film
            var film =
                await _db.SingleAsync<Film, FilmDTO>(c => c.Id.Equals(id));

            //Return the result in the films variable wrapped in a call to the Results. Ok response status method.
            return Results.Ok(film);

        }
        catch (Exception)
        {
        }
        return Results.NotFound();
    }

    // POST api/<FilmsController>
    /*The AddAsync method in the DbService adds the provided film asynchronously when called from the FilmController’s HttpPost action method. Don’t forget to call the SaveChangesAsync method 
     *to persist the data in the table. Return the added film and its URI with the Created response method.

     *Using the FilmDTO will cereate a recursive loop which crashes the API call and results in an error. To avoid this, you add a second DTO named FilmCreateDTO without Id and navigation properties
     *to the FDBL.Common project’s DTOs folder. The Put method has the same problem. Add a class named FilmEditDTO, which inherits the FilmCreateDTO and has an Id property.*/
    [HttpPost]
    public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
    {
        try
        {
            //return BadRequest if the dto parameter is null.
            if (dto == null) return Results.BadRequest();

            /*Await the result from a call to the AddAsync method on the _db service instance and specify the Film entity and FilmCreateDTO as its types. Store the result in a variable named film.*/
            var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);

            /*Call the SaveChangesAsync method to persist the film in the table and save the result in a variable named success.*/
            var success = await _db.SaveChangesAsync();

            //Return BadRequest if the success parameter is false.
            if (!success)
            {
                return Results.BadRequest();
            }

            //Return the film’s URI and the film wrapped in a call to the Results.Created response status method.
            return Results.Created(_db.GetURI<Film>(film), film);
        }
        catch
        {
        }

        return Results.BadRequest();
    }

    // PUT api/<FilmsController>/5
    /*The Update method in the DbService modifies the provided course when called from the FilmsController’s HttpPut action method. Don’t forget to call the SaveChangesAsync method to 
     *persist the chaanges in the table. Return no content with the NoContent response method.

     *Because the FilmDTO will cereate a recursive loop which crashes the API call and results in an error, use the FilmEditDTO instead*/
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
    //The DeleteAsync method in the DbService removes the film for the provided film id when called from the FilmsController’s HttpDelete action method.Don’t forget to call the SaveChangesAsync
    //method to persist the chaanges in the table. Return no content with the NoContent response method.
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            //Inside the try block, call the DeleteAsync method on the _db service instance and specify the Film entity as its type.Save there result in a variable named success.
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
