using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace SportsStore.Infrastructure;

public static class SessionExtentions
{
    public static void SetJson(this ISession session, string key, object value)
    {
        session.SetString(key, (string)JsonSerializer.Serialize(value));
    }
    public static T? GetJson<T>(this ISession session, string key)
    {
        var SessionData = session.GetString(key);
        return SessionData == null 
        ? default(T)
        : JsonSerializer.Deserialize<T>(SessionData);
    }
}
