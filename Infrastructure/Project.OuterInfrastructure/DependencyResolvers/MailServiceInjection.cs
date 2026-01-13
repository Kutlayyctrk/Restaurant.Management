using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Project.Application.MailService;
using Project.OuterInfrastructure.Tools;

namespace Project.OuterInfrastructure.DependencyResolvers
{
    public static class MailServiceInjection
    {
        public static void AddMailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));

            services.AddScoped<IMailSender, MailSender>();
        }
    }
}