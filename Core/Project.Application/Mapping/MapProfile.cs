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
            CreateMap<Product, ProductDTO>()
      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
      .ReverseMap()
      .ForMember(dest => dest.Category, opt => opt.Ignore());
            CreateMap<RecipeDTO, Recipe>()
      .ForMember(dest => dest.RecipeItems, opt => opt.MapFrom(src => src.RecipeItems))
      .ReverseMap();

            CreateMap<RecipeItemDTO, RecipeItem>().ReverseMap();

            CreateMap<AppUserDTO, AppUser>().ReverseMap();
            CreateMap<AppUserRoleDTO, AppUserRole>().ReverseMap();
            CreateMap<AppUserProfileDTO, AppUserProfile>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();


            CreateMap<OrderDTO, Order>().ReverseMap();


            CreateMap<OrderDetailDTO, OrderDetail>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<StockTransActionDTO, StockTransAction>().ReverseMap();
            CreateMap<TableDTO, Table>().ReverseMap();
            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<UnitDTO, Unit>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<MenuProduct, MenuProductDTO>()
          .ForMember(dest => dest.MenuName, opt => opt.MapFrom(src => src.Menu.MenuName))
          .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
          .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product.Category.CategoryName))
          .ReverseMap()
          .ForMember(dest => dest.Menu, opt => opt.Ignore())
          .ForMember(dest => dest.Product, opt => opt.Ignore());






        }
    }
}