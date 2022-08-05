using Newtonsoft.Json;

namespace ToDo.Domain
{
    public static class Extensions
    {
        public static string Serialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T? Deserialize<T>(this string data)
        {
            T? result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }
    }
}
