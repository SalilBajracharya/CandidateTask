using CandidateTask.API.Filters;
using CandidateTask.API.Results;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace CandidateTask.API
{
    public static class ConfigureService
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            AspNetCoreResult.Setup(c => c.DefaultProfile = new FluentResultFilterProfile());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers(options =>
            {
                options.Filters.Add<ModelStateFilter>();
                options.Filters.Add<CustomExceptionHandlerFilters>();
            });

            // Customize default API behavior
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true
            );

            return services;
        }
    }
}
