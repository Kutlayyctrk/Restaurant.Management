

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Project.API.Middleware;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using Project.Persistance.DependencyResolvers;
using Project.Application.DependencyResolvers;
using Project.InnerInfrastructure.DependencyResolvers;
using Project.OuterInfrastructure.DependencyResolvers;
using Project.Validator.DependencyResolvers;
using Serilog;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace Project.API
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
                .WriteTo.File("Logs/api-log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
            Log.Information("API uygulamasý baþlatýlýyor...");

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

            
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // In-Memory Caching
            builder.Services.AddMemoryCache();

            // Rate Limiting
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter("fixed", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 100;
                    limiterOptions.Window = TimeSpan.FromMinutes(1);
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    limiterOptions.QueueLimit = 10;
                });
            });

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

           
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            WebApplication app = builder.Build();

            // Docker container içinde çalýþýrken veritabanýný otomatik oluþtur/güncelle
            if (runningInContainer)
            {
                using (IServiceScope scope = app.Services.CreateScope())
                {
                    MyContext db = scope.ServiceProvider.GetRequiredService<MyContext>();
                    ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    try
                    {
                        logger.LogInformation("Applying database migrations...");
                        db.Database.Migrate();
                        logger.LogInformation("Database migrations applied successfully.");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred while applying database migrations.");
                        throw;
                    }
                }
            }

            
            // Global Exception Handler
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers().RequireRateLimiting("fixed");

            app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "API uygulamasý baþlatýlýrken hata oluþtu.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
