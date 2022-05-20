namespace ExadelTimeTrackingSystem.BusinessLogic.Mappings
{
    using System;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.Data.Models;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
