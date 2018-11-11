using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Services;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Infrastructure;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Mapping
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ApplicationDbFactory>();
            serviceCollection.AddTransient<IMemeRepository, MemeRepository>(serviceProvider =>
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                return new MemeRepository(applicationDbFactory.CreateNewInstance());
            });
            serviceCollection.AddTransient<IExternalMemeService, ExternalMemeService>();
            serviceCollection.AddTransient<ILocalMemeService, LocalMemeService>();
            using (ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider())
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                serviceCollection.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(applicationDbFactory.GetConnectionString()), 
                    ServiceLifetime.Transient);
            }
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            serviceCollection.AddTransient<IUserService, UserService>(serviceProvider =>
            {
                UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                SignInManager<ApplicationUser> signInManager = serviceProvider.GetService<SignInManager<ApplicationUser>>();
                return new UserService(signInManager, userManager);
            });

            // TODO: Move this configuration
            serviceCollection.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });

            // TODO: Move routing to application layer
            serviceCollection.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";

                options.ExpireTimeSpan = TimeSpan.FromDays(3);
            });

            return serviceCollection;
        }
    }
}
