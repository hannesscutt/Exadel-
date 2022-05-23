namespace ExadelTimeTrackingSystem.BusinessLogic.Extensions
{
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.Mappings;

    public static class MapperExtensions
    {
        public static IMapper Mapper { get; }

        static MapperExtensions()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectProfile());
                mc.AddProfile(new TaskProfile());
                mc.AddProfile(new UserProfile());
            });
            mapperConfig.AssertConfigurationIsValid();
            Mapper = mapperConfig.CreateMapper();
        }
    }
}
