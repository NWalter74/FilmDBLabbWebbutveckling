namespace FDBL.Common.HttpClients;

public class MembershipHttpClient
{
    public HttpClient Client { get; }

    //Add a inject an instance of the HttpClient class into constructor and store it in class-level 
    //variable named Client
    public MembershipHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
    }

    public void AddBearerToken(string token)
    {
        Client.DefaultRequestHeaders.Remove("Authorization");
        Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    }
}
