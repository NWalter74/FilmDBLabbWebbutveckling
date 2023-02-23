namespace FDBL.Membership.API.Utilites;

public static class JsonUtility
{
    public static T RemoveLoops<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions()
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        });
        return JsonSerializer.Deserialize<T>(json);
    }
}