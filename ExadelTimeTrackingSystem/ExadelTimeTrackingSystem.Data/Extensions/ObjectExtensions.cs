namespace ExadelTimeTrackingSystem.Data.Extensions
{
    using System.Text.Json;

    public static class ObjectExtensions
    {
        public static T DeepCopy<T>(this T source)
        {
            var serialized = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(serialized);
        }
    }
}
