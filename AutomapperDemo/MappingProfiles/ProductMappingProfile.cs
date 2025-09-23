using AutoMapper;
using AutomapperDemo.DTOs;
using AutomapperDemo.Models;

namespace AutomapperDemo.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile ()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(
            dest => dest.ProductName,
            opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.ShortDescription,
                opt => opt.MapFrom(src => src.Description)
            );

        CreateMap<ProductCreateDTO, Product>();

    }

}
