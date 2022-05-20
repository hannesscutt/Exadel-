namespace ExadelTimeTrackingSystem.BusinessLogic.Mappings
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDTO>();
            CreateMap<Status, StatusDTO>();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<CreateTaskDTO, Task>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
