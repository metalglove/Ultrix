using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Infrastructure.Services;
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

            // TODO: check if this is still needed
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
                options.LoginPath = "/Login";

                options.ExpireTimeSpan = TimeSpan.FromDays(3);
            });

            #region Validators
            serviceCollection.AddTransient<IEntityValidator<Follower>, FollowerValidator>();
            serviceCollection.AddTransient<IEntityValidator<Collection>, CollectionValidator>();
            serviceCollection.AddTransient<IEntityValidator<SharedMeme>, SharedMemeValidator>();
            serviceCollection.AddTransient<IEntityValidator<Meme>, MemeValidator>();
            #endregion Validators

            #region Repositories
            serviceCollection.AddTransient<IMemeRepository, MemeRepository>(serviceProvider =>
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                IEntityValidator<Meme> entityValidator = serviceProvider.GetService<IEntityValidator<Meme>>();
                return new MemeRepository(applicationDbFactory.CreateNewInstance(), entityValidator);
            });
            serviceCollection.AddTransient<IFollowerRepository, FollowerRepository>(serviceProvider =>
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                IEntityValidator<Follower> entityValidator = serviceProvider.GetService<IEntityValidator<Follower>>();
                return new FollowerRepository(applicationDbFactory.CreateNewInstance(), entityValidator);
            });
            serviceCollection.AddTransient<ICollectionRepository, CollectionRepository>(serviceProvider =>
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                IEntityValidator<Collection> entityValidator = serviceProvider.GetService<IEntityValidator<Collection>>();
                return new CollectionRepository(applicationDbFactory.CreateNewInstance(), entityValidator);
            });
            serviceCollection.AddTransient<ISharedMemeRepository, SharedMemeRepository>(serviceProvider =>
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                IEntityValidator<SharedMeme> entityValidator = serviceProvider.GetService<IEntityValidator<SharedMeme>>();
                return new SharedMemeRepository(applicationDbFactory.CreateNewInstance(), entityValidator);
            });
            serviceCollection.AddTransient<IUserRepository, UserRepository>(serviceProvider =>
            {
                ApplicationDbFactory applicationDbFactory = serviceProvider.GetService<ApplicationDbFactory>();
                return new UserRepository(applicationDbFactory.CreateNewInstance());
            });
            #endregion Repositories

            #region Services
            serviceCollection.AddTransient<IExternalMemeFetcherService, ExternalMemeFetcherService>();
            serviceCollection.AddTransient<ILocalMemeFetcherService, LocalMemeFetcherService>();
            serviceCollection.AddTransient<IMemeService, MemeService>(serviceProvider =>
            {
                IMemeRepository memeRepository = serviceProvider.GetService<IMemeRepository>();
                ISharedMemeRepository sharedMemeRepository = serviceProvider.GetService<ISharedMemeRepository>();
                ILocalMemeFetcherService memeFetcherService = serviceProvider.GetService<ILocalMemeFetcherService>();
                IUserService userService = serviceProvider.GetService<IUserService>();
                return new MemeService(memeFetcherService, memeRepository, sharedMemeRepository, userService);
            });
            serviceCollection.AddTransient<ICollectionService, CollectionService>(serviceProvider =>
            {
                IMemeRepository memeRepository = serviceProvider.GetService<IMemeRepository>();
                ICollectionRepository collectionRepository = serviceProvider.GetService<ICollectionRepository>();
                return new CollectionService(collectionRepository, memeRepository);
            });
            serviceCollection.AddTransient<IUserService, UserService>(serviceProvider =>
            {
                IUserRepository userRepository = serviceProvider.GetService<IUserRepository>();
                UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                SignInManager<ApplicationUser> signInManager = serviceProvider.GetService<SignInManager<ApplicationUser>>();
                return new UserService(signInManager, userManager, userRepository);
            });
            serviceCollection.AddTransient<IFollowerService, FollowerService>(serviceProvider =>
            {
                IFollowerRepository followerRepository = serviceProvider.GetService<IFollowerRepository>();
                return new FollowerService(followerRepository);
            });
            #endregion Services

            return serviceCollection;
        }
    }
}
