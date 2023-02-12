using System.Text.Json;
using System.Text;

namespace FDBL.Common.Services;

public class AdminService : IAdminService
{
    readonly MembershipHttpClient _http;

    /*Inject an instance of the MembershipHttpClient into its constrcutor and store it in a classlever variable named _http. 
      This HttpClient is configured to call controllers in the membership API.*/
    public AdminService(MembershipHttpClient httpClient)
    {
        _http = httpClient;
    }

    /*Add a generic method named GetAsync<TDto>, which returns a list of TDto objects and a 
    string parameter named uri.
    Add a try/catch where the catch throws the exception up the call chain to the calling 
    method.*/
    public async Task<List<TDto>> GetAsync<TDto>(string uri)
    {
        try
        {
            /*Await a call to the _http.Clinet.GetAsync method in the MembershipHttpClient with the URI 
            in the uri parameter targeting the HttpGet method in the API’s CoursesController controller, 
            returning all records for the entity. Store the result in an HttpResponseMessage variable 
            named response.
            
            Call the EnsureSuccessStatusCode method on the response object to ensure the call was 
            successful, else it throes an exception handled by the catch.*/
            using HttpResponseMessage response = await _http.Client.GetAsync(uri);// $"courses?freeOnly=false");
            response.EnsureSuccessStatusCode();

            /*Call the JsonSerializer.Deserialze method and specify List<TDto> as its generic type you 
            want to transform the JSON data returned by the API into. Pass in the response content as a
            stream to the method Store the result n a variable named result. Because JSON uses 
            camelcaas and C# Pascal case, you must specify that property names are case insensitive for 
            the desrialzer to match the property names.
            */
            var result = JsonSerializer.Deserialize<List<TDto>>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //Return an empty List<TDto> if the result variable is null, otherwise return result
            return result ?? new List<TDto>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TDto?> SingleAsync<TDto>(string uri)
    {
        try
        {
            using HttpResponseMessage response = await _http.Client.GetAsync(uri);//$"films/{id}");
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<TDto>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? default;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task CreateAsync<TDto>(string uri, TDto dto)
    {
        try
        {
            /*Call the JsonSerializer.Serialize method with the dto parameter and sprcify UTF8 encoding 
            and application/json as the media type.*/
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json");

            /*Call the CoursesController’s post method with the HttpClient’s PostAsync method, passing it 
            the URI in the uri parameter and the jsonContent variable.*/
            using HttpResponseMessage response = await _http.Client.PostAsync(uri, jsonContent); //"films", jsonContent);

            //Call the EnsureSuccessStatusCode method on the response object to check for errors.
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task EditAsync<TDto>(string uri, TDto dto)
    {
        try
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await _http.Client.PutAsync(uri, jsonContent); //$"films/{id}", jsonContent);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync<TDto>(string uri)
    {
        try
        {
            using HttpResponseMessage response = await _http.Client.DeleteAsync(uri); //$"films/{id}");

            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            throw;
        }
    }

}
