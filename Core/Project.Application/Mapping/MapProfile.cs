using AutoMapper;
using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;

namespace Project.Application.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppRoleDTO, AppRole>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<RecipeDTO, Recipe>().ReverseMap();
            CreateMap<RecipeItemDTO, RecipeItem>().ReverseMap();
            CreateMap<AppUserDTO, AppUser>().ReverseMap();
            CreateMap<AppUserRoleDTO, AppUserRole>().ReverseMap();
            CreateMap<AppUserProfileDTO, AppUserProfile>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();

            // Order mapping
            CreateMap<OrderDTO, Order>().ReverseMap();

            // OrderDetail mapping (ProductName navigation’dan geliyor)
            CreateMap<OrderDetailDTO, OrderDetail>()
                .ForMember(dest => dest.Product, opt => opt.Ignore()) // DTO'dan Product entity'ye map etmiyoruz
                .ReverseMap()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<StockTransActionDTO, StockTransAction>().ReverseMap();
            CreateMap<TableDTO, Table>().ReverseMap();
            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<UnitDTO, Unit>().ReverseMap();
        }
    }
}