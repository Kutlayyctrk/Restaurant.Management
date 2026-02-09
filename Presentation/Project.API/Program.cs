
using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using Project.Persistance.DependencyResolvers;
using Project.Application.DependencyResolvers;
using Project.InnerInfrastructure.DependencyResolvers;
using Project.OuterInfrastructure.DependencyResolvers;
using Project.Validator.DependencyResolvers;
using System.Text.Json.Serialization;

namespace Project.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

           
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

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
