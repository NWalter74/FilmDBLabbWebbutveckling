using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
using System.Reflection.Metadata;
using System.Data.Common;

namespace FDBL.Admin.UI.Authentication;

public class SessionStorageService : IStorageService    //Implement the IStorageService interface to add its methods, Add async to the three methods
{
    private readonly ProtectedSessionStorage _sessionStorage;

    //Add a constructor and inject ProtectedSessionStorage to get access to Session Storage and store the instance in a varaible named _sessionStorage.

    public SessionStorageService(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    /*Replace the exception in the SetAsync method with a call to the _sessionStorage instance’s SetAsync method with the key and value parameters to store the value in the value
     *parameter under the name in the key parameter*/
    public async Task SetAsync(string key, string value) => await _sessionStorage.SetAsync(key, value);

    //Replace the exception in the GetAsync method with a try/catch and return an empty string below the catch.
    public async Task<string> GetAsync(string key)
    {
        try
        {
            //call to the _sessionStorage instance’s GetAsync method with the key parameter and store the result in a varaible named result.
            var result = await _sessionStorage.GetAsync<string>(key);

            //If the result variable’s Success property is true, return the value in the result variable’s Value property if it is not null, otherwise return an empty string.
            if (result.Success)
                return result.Value ?? string.Empty;
        }
        catch
        {
        }

        return string.Empty;
    }

    //Replace the exception in the RemoveAsync method with a call to the _sessionStorage instance’s DeleteAsync method with the key parameter to remove its corresponding value
    public async Task RemoveAsync(string key) => await _sessionStorage.DeleteAsync(key);
}
