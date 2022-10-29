using System.Text.Json;

namespace LearnRocky.Utils
{
    public static class SessionExt
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Read<T>(this ISession session, string key)
        {
            var res = session.GetString(key);
            return (res == null) ? default : JsonSerializer.Deserialize<T>(res);
        }
    }
}
