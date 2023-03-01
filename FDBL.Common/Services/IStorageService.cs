using static System.Formats.Asn1.AsnWriter;

namespace FDBL.Common.Services;

public interface IStorageService
{
    //Add a definition for a Task<string> method named GetAsync with a string parameter named key, which represent the name of the value to fetch from storage.
    Task<string> GetAsync(string key);

    //Add a definition for a Task method named SetAsync with two string parameters named key, which represent the name of the value to store, and value, which is the value to store.
    Task SetAsync(string key, string value);

    //Add a definition for a Task method named RemoveAsync with a string parameter named key, which represent the name of the value to remove from storage.
    Task RemoveAsync(string key);
}
