using FDBL.Common.Classes;
using System.Text;
using FDBL.Common.Models;

namespace FDBL.Common.HttpClients;

public class UserHttpClient
{
    public HttpClient Client { get; }

    //Add a inject an instance of the HttpClient class into constructor and store it in class-level 
    //variable named Client
    public UserHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
    }

    public async Task CreateUser(CreateUserModel model)
    {
        try
        {
            if (model == null) throw new ArgumentException("CreateUserModel is null.");

            var roles = new List<string> { UserRole.Registered };
            if (model.IsCustomer) roles.Add(UserRole.Customer);
            if (model.IsAdmin) roles.Add(UserRole.Admin);

            var user = new RegisterUserDTO(model.Email, model.Password, roles);

            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8,
                    "application/json");

            using HttpResponseMessage response = await Client.PostAsync("api/users/register", jsonContent);
            if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
        }
        catch
        {
            throw;
        }
    }

    public async Task PayingCustomer(PaidCustomerDTO model)
    {
        try
        {
            if (model == null) throw new ArgumentException("PaidCustomerDTO is null.");

            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(model),
                    Encoding.UTF8,
                    "application/json");

            using HttpResponseMessage response = await Client.PostAsync("users/paid", jsonContent);
            if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
