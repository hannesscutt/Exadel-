namespace ExadelTimeTrackingSystem.BusinessLogic.Mappings
{
    using System;
    using AutoMapper;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.Data.Models;
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
