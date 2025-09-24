using AutoMapper;
using AutomapperDemo.DTOs;
using AutomapperDemo.Models;

namespace AutomapperDemo.MappingProfiles;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile ()
    {
        CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
            .ReverseMap()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => GetFirstName(src.FullName)))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => GetLastName(src.FullName)));
    }

    private static string GetFirstName ( string fullName )
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return string.Empty;
        return fullName.Split(' ')[0];
    }

    private static string GetLastName ( string fullName )
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return string.Empty;
        var names = fullName.Split(' ');
        return names.Length > 1 ? string.Join(" ", names.Skip(1)) : string.Empty;
    }

}
