using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTask.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                    );
            return services;
        }
    }
}
