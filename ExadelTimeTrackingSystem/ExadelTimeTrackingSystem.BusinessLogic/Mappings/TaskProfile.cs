namespace ExadelTimeTrackingSystem.BusinessLogic.Mappings
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDTO>();
            CreateMap<CreateTaskDTO, Task>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<CreateTaskDTO, Task>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
