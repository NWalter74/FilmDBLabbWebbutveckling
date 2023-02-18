using FDBL.Common.DTOs;

namespace FDBL.Common.Services;

public class MembershipService : IMembershipService
{
    protected readonly MembershipHttpClient _http;

    public MembershipService(MembershipHttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<List<FilmDTO>> GetFilmsAsync()
    {
        try
        {
            bool freeOnly = false;

            using HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<FilmDTO>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new List<FilmDTO>();

        }
        catch (Exception)
        {
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
