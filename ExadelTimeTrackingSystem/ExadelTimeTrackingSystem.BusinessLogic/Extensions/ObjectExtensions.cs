namespace ExadelTimeTrackingSystem.BusinessLogic.Extensions
{
    using Newtonsoft.Json;

    public static class ObjectExtensions
    {
        static ObjectExtensions()
        {
        }

        public static T DeepCopy<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
