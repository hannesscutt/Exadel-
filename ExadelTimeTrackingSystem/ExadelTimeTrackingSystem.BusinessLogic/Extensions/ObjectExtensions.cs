namespace ExadelTimeTrackingSystem.BusinessLogic.Extensions
{
    using Newtonsoft.Json;
    using System.Text.Json;

    public static class ObjectExtensions
    {
        public static T DeepCopy<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
