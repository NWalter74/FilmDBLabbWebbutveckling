namespace FDBL.Membership.API.Controllers;

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

                var films = freeOnly ?
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
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<FilmsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<FilmsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<FilmsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
