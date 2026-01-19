using Microsoft.Extensions.DependencyInjection;
using Project.Application.Managers;
using Project.InnerInfrastructure.ManagerConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.DependencyResolvers
{
    public static class ManagerServiceInjection
    {
        public static void AddManagerService(this IServiceCollection services)
        {
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppUserRoleManager, AppUserRoleManager>();
            services.AddScoped<IAppUserProfileManager, AppUserProfileManager>();
            services.AddScoped<IAppRoleManager, AppRoleManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IRecipeManager, RecipeManager>();
            services.AddScoped<IRecipeItemManager, RecipeItemManager>();
            services.AddScoped<IOrderDetailManager, OrderDetailManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IStockTransActionManager, StockTransActionManager>();
            services.AddScoped<IUnitManager, UnitManager>();
            services.AddScoped<ITableManager, TableManager>();
            services.AddScoped<ISupplierManager, SupplierManager>();
            services.AddScoped<IMenuManager, MenuManager>();
            services.AddScoped<IMenuProductManager, MenuProductManager>();
            




        }

    }
}
