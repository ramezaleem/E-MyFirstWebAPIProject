using AutoMapper;

namespace AutomapperDemo.MappingProfiles;

public class UserMappingProfile : Profile
{
    //public UserMappingProfile ()
    //{
    //    CreateMap<User, UserDTO>()
    //        .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
    //          .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
    //            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
    //            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));

    //    CreateMap<UserCreateDTO, User>()
    //     .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
    //     {
    //         Street = src.Street,
    //         City = src.City,
    //         State = src.State,
    //         ZipCode = src.ZipCode
    //     }));
    //}

}