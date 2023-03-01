namespace FDBL.Common.Services;

public class MembershipService : IMembershipService
{
    protected readonly MembershipHttpClient _http;
    private readonly IStorageService _storage;
    protected readonly ILocalStorageService _localStorage;

    public MembershipService(MembershipHttpClient httpClient, IStorageService storage, ILocalStorageService localStorage)
    {
        _http = httpClient;
        _storage = storage;
        _localStorage = localStorage;
    }

    public async Task<List<FilmDTO>> GetFilmsAsync()
    {
        try
        {
            var token = await _storage.GetAsync(AuthConstants.TokenName);
            bool freeOnly = JwtParser.ParseIsNotInRoleFromPayload(token, UserRole.Customer);

            _http.AddBearerToken(token);

            /*Await a call to the _http.Clinet.GetAsync method in the MembershipHttpClient with the film URI and its freeOnly parameter ($"films?freeOnly={freeOnly}") to target the API’s 
             *FilmsController controller. Store the result in an HttpResponseMessage variable named response.
             *Call the EnsureSuccessStatusCode method on the response object to ensure the call was successful, else it throws an exception handled by the catch*/
            using HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            /*Call the JsonSerializer.Deserialze method and specify List<FilmDTO> as its generic type you want to transform the JSON data returned by the API into. Pass in the response content 
             *as a stream to the method and store the result in a variable named result. Because JSON uses camelcase and C# Pascal case, you must specify that property names are case 
             *insensitive for the desrialzer to match the property names.*/
            var result = JsonSerializer.Deserialize<List<FilmDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //Return an empty List<FilmDTO> if the result variable is null, otherwise return result.
            return result ?? new List<FilmDTO>();

        }
        catch (Exception)
        {
            //This avoids null check logic in the dashboard’s Razor page as the method always returns list
            return new List<FilmDTO>();
        }
    }

    public async Task<FilmDTO> GetFilmAsync(int? id)
    {
        try
        {
            if (id == null)
            {
                new FilmDTO();
            }

            using HttpResponseMessage response = await _http.Client.GetAsync($"films/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmDTO>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new FilmDTO();

        }
        catch (Exception)
        {
            return new FilmDTO();
        }
    }

    public async Task<GenreDTO> GetGenreAsync(int? id)
    {
        try
        {
            if (id is null) return new GenreDTO();
            using HttpResponseMessage response = await _http.Client.GetAsync($"genre/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<GenreDTO>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new GenreDTO();
        }
        catch
        {
            return new GenreDTO();
        }
    }

    public async Task<List<GenreDTO>> GetGenresAsync()
    {
        try
        {
            using HttpResponseMessage response = await _http.Client.GetAsync($"genres");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<GenreDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new List<GenreDTO>();

        }
        catch (Exception)
        {
            return new List<GenreDTO>();
        }
    }


    public async Task<SimilarFilmDTO> GetSimilarFilmAsync(int? id)
    {
        try
        {
            if (id == null)
            {
                new SimilarFilmDTO();
            }

            using HttpResponseMessage response = await _http.Client.GetAsync($"similarFilm/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<SimilarFilmDTO>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new SimilarFilmDTO();

        }
        catch (Exception)
        {
            return new SimilarFilmDTO();
        }

    }

    public async Task<List<SimilarFilmDTO>> GetSimilarFilmsAsync()
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"similarFilms");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<SimilarFilmDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new List<SimilarFilmDTO>();

        }
        catch (Exception)
        {
            return new List<SimilarFilmDTO>();
        }
    }

    public async Task<FilmGenreDTO> GetFilmGenreAsync(int? id)
    {
        try
        {
            if (id == null)
            {
                new FilmGenreDTO();
            }

            using HttpResponseMessage response = await _http.Client.GetAsync($"filmGenre/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<FilmGenreDTO>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new FilmGenreDTO();

        }
        catch (Exception)
        {
            return new FilmGenreDTO();
        }
    }

    public async Task<List<FilmGenreDTO>> GetFilmGenresAsync()
    {
        try
        {

            using HttpResponseMessage response = await _http.Client.GetAsync($"filmGenres");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmGenreDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new List<FilmGenreDTO>();

        }
        catch (Exception e)
        {
            return new List<FilmGenreDTO>();
        }
    }
}
