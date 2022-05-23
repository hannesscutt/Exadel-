namespace ExadelTimeTrackingSystem.WebAPI.Configuration
{
    using ExadelTimeTrackingSystem.Data;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;

    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
