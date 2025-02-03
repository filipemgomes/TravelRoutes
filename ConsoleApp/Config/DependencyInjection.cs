using Application.UseCases;
using Domain.Repositories;
using InfraStructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IRouteRepository, RouteRepository>();

            services.AddScoped<AddRouteUseCase>();
            services.AddScoped<GetBestRouteUseCase>();

            return services;
        }
    }
}
