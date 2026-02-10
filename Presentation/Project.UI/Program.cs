using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using Project.Persistance.DependencyResolvers;
using Project.Application.DependencyResolvers;
using Project.OuterInfrastructure.DependencyResolvers;
using Project.InnerInfrastructure.DependencyResolvers;
using Project.UI.Middleware;
using Project.Validator.DependencyResolvers;
using Serilog;

namespace Project.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.File("Logs/ui-log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
            Log.Information("UI uygulaması başlatılıyor...");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            bool runningInContainer = string.Equals(
                Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
                "true", StringComparison.OrdinalIgnoreCase);

            if (runningInContainer)
            {
                string? dockerCs = builder.Configuration.GetConnectionString("OnionDb_Docker");
                if (!string.IsNullOrWhiteSpace(dockerCs))
                {
                    builder.Configuration["ConnectionStrings:OnionDb"] = dockerCs;
                }
            }

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            // In-Memory Caching
            builder.Services.AddMemoryCache();

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
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseSerilogRequestLogging();

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
            catch (Exception ex)
            {
                Log.Fatal(ex, "UI uygulaması başlatılırken hata oluştu.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
 