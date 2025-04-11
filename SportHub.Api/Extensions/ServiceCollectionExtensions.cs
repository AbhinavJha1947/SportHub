// SportHub.Api/Extensions/ServiceCollectionExtensions.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportHub.Contracts.RepositoryInterfaces;
using SportHub.Contracts.ServiceInterfaces;
using SportHub.Data.Context;
using SportHub.Data.Repositories;
using SportHub.Services;
using AutoMapper;

namespace SportHub.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<SportHubDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            // Services
            services.AddScoped<IPlayerService, PlayerService>();

            // Register AutoMapper manually instead of using the extension method
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Services.Mappings.MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}