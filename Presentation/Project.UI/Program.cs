using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using Project.Persistance.DependencyResolvers;
using Project.Application.DependencyResolvers;
using Project.OuterInfrastructure.DependencyResolvers;
using Project.InnerInfrastructure.DependencyResolvers;
using Project.Validator.DependencyResolvers;

namespace Project.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            builder.Services.AddDbContextInjection(builder.Configuration);
            builder.Services
               .AddIdentity<AppUser, AppRole>(options =>
               {
                   options.User.RequireUniqueEmail = true;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireLowercase = false;
               })
               .AddEntityFrameworkStores<MyContext>()
               .AddDefaultTokenProviders();

            builder.Services.AddRepositoryServices();
            builder.Services.AddMapperService();
            builder.Services.AddManagerService();
            builder.Services.AddValidationServices();
            builder.Services.AddMailService(builder.Configuration);

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LoginAndRegister}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
 