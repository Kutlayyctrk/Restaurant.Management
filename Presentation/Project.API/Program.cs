

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.API.Middleware;
using Project.API.Models;
using Project.API.Services;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using Project.Persistance.DependencyResolvers;
using Project.Application.DependencyResolvers;
using Project.InnerInfrastructure.DependencyResolvers;
using Project.OuterInfrastructure.DependencyResolvers;
using Project.Validator.DependencyResolvers;
using Serilog;
using System.Text;
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
            Log.Information("API uygulaması başlatılıyor...");

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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant Management API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT token'ınızı buraya girin. Örnek: eyJhbGciOiJIUzI1NiIs..."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

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

            // JWT Authentication
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddScoped<ITokenService, TokenService>();

            JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

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

            // Docker container içinde çalışırken veritabanını otomatik oluştur/güncelle
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
                Log.Fatal(ex, "API uygulaması başlatılırken hata oluştu.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
