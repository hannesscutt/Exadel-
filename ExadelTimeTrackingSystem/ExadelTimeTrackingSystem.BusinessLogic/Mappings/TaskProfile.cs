﻿namespace ExadelTimeTrackingSystem.BusinessLogic.Mappings
{
    using System;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDTO>();
            CreateMap<Status, StatusDTO>()
                .ReverseMap();
            var map = CreateMap<CreateTaskDTO, Task>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            map
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.Date));
        }
    }
}
