using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Project.Validator.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Validator.DependencyResolvers
{
    public static class ValidationServiceInjection
    {
        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AppRoleValidator>(includeInternalTypes :true);
        }
    }
}
