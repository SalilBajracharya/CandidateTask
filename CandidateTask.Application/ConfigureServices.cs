using System.Reflection;
using CandidateTask.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTask.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                    );

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
    }
}
