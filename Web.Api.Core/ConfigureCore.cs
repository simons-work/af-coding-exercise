﻿using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Core.Services;
using Web.Api.Core.Validators;

namespace Web.Api.Core
{
    public static class ConfigureCore
    {
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CustomerDtoValidator>());
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
