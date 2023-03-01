namespace FDBL.Common.Services;

public class LocalStorageService : IStorageService      //Implement the IStorageService interface to add its methods, Add async to the three methods
{
    private readonly ILocalStorageService _localStorage;

    //Add a constructor and inject ILocalStorageService to get access to Local Storage and store the instance in a varaible named _localStorage.
    public LocalStorageService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    //Replace the exception in the GetAsync method with a call to the _localStorage instance’s GetItemAsync method with the key parameter and return the result
    public async Task<string> GetAsync(string key)
    {
        return await _localStorage.GetItemAsync<string>(key);
    }

    //Replace the exception in the SetAsync method with a call to the _localStorage instance’s SetItemAsync method with the key and value parameters to store the value in the value
    //parameter under the name in the key parameter.
    public async Task RemoveAsync(string key)
    {
        await _localStorage.RemoveItemAsync(key);
    }

    //Replace the exception in the RemoveAsync method with a call to the _localStorage instance’s RemoveItemAsync method with the key parameter its corresponding value.
    public async Task SetAsync(string key, string value)
    {
        await _localStorage.SetItemAsync(key, value);
    }
}