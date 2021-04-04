using Microsoft.Extensions.DependencyInjection;
using SOAnswerThis.Infrastructure.Services;
using SOAnswerThis.Infrastructure.Services.Abstract;

namespace SOAnswerThis.Infrastructure.IoC
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStackOverflowService, StackOverflowService>();
            return services;
        }
    }
}