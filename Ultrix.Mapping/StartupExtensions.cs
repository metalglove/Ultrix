using Microsoft.Extensions.DependencyInjection;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Mapping
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MemesDbFactory>();
            serviceCollection.AddTransient<IMemeRepository, MemesDbContext>((serviceProvider) =>
            {
                MemesDbFactory memesDbFactory = serviceProvider.GetService<MemesDbFactory>();
                return memesDbFactory.CreateNewInstance();
            });
            serviceCollection.AddTransient<IExternalMemeService, ExternalMemeService>();
            serviceCollection.AddTransient<ILocalMemeService, LocalMemeService>();
            return serviceCollection;
        }
    }
}
