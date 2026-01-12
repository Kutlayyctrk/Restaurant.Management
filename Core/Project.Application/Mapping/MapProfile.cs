using AutoMapper;
using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Mapping
{
    public class MapProfile:Profile
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
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
            CreateMap<StockTransActionDTO, StockTransAction>().ReverseMap();
            CreateMap<TableDTO, Table>().ReverseMap();
            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<UnitDTO, Unit>().ReverseMap();
        }
    }
}
