using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ultrix.Application.Interfaces;
using Ultrix.Infrastructure;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Mapping
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            const string connection = @"Server = (localdb)\mssqllocaldb; Database = MemesDbContext; Trusted_Connection = True; ConnectRetryCount = 0";
            serviceCollection.AddDbContext<MemesDbContext>(options => options.UseSqlServer(connection));
            serviceCollection.AddTransient<IExternalMemeService, ExternalMemeService>();
            serviceCollection.AddTransient<ILocalMemeService, LocalMemeService>();
            return serviceCollection;
        }
    }
}
