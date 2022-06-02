namespace ExadelTimeTrackingSystem.BusinessLogic.Extensions
{
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.Data.Models;
    using Newtonsoft.Json;

    public static class CopyExtension
    {
        static CopyExtension()
        {
        }

        public static T Copy<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
