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
            serviceCollection.AddSingleton<IFactory<ApplicationDbContext>, ApplicationDbFactory>();

            using (ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider())
            {
                // TODO: Needs the connection string from simple builder instead of the factory directly
                ApplicationDbFactory applicationDbFactory = (ApplicationDbFactory)serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                serviceCollection.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlServer(applicationDbFactory.GetConnectionString()),
                    ServiceLifetime.Transient);
            }

            // TODO: Create scratch implementation of UserManager
            serviceCollection.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            serviceCollection.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });

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
            serviceCollection.AddTransient<IEntityValidator<MemeLike>, MemeLikeValidator>();
            serviceCollection.AddTransient<IEntityValidator<CollectionItemDetail>, CollectionItemDetailValidator>();
            serviceCollection.AddTransient<IEntityValidator<CollectionSubscriber>, CollectionSubscriberValidator>();
            serviceCollection.AddTransient<IEntityValidator<ApplicationUser>, ApplicationUserValidator>();
            serviceCollection.AddTransient<IEntityValidator<Comment>, CommentValidator>();
            #endregion Validators

            #region Repositories
            serviceCollection.AddTransient<IRepository<Meme>, MemeRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<Meme> entityValidator = serviceProvider.GetService<IEntityValidator<Meme>>();
                return new MemeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Follower>, FollowerRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<Follower> entityValidator = serviceProvider.GetService<IEntityValidator<Follower>>();
                return new FollowerRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Collection>, CollectionRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<Collection> entityValidator = serviceProvider.GetService<IEntityValidator<Collection>>();
                return new CollectionRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<SharedMeme>, SharedMemeRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<SharedMeme> entityValidator = serviceProvider.GetService<IEntityValidator<SharedMeme>>();
                return new SharedMemeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<ApplicationUser>, UserRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<ApplicationUser> entityValidator = serviceProvider.GetService<IEntityValidator<ApplicationUser>>();
                return new UserRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<MemeLike>, MemeLikeRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<MemeLike> entityValidator = serviceProvider.GetService<IEntityValidator<MemeLike>>();
                return new MemeLikeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<CollectionItemDetail>, CollectionItemDetailRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<CollectionItemDetail> entityValidator = serviceProvider.GetService<IEntityValidator<CollectionItemDetail>>();
                return new CollectionItemDetailRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<CollectionSubscriber>, CollectionSubscriberRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<CollectionSubscriber> entityValidator = serviceProvider.GetService<IEntityValidator<CollectionSubscriber>>();
                return new CollectionSubscriberRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Comment>, CommentRepository>(serviceProvider =>
            {
                IFactory<ApplicationDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<ApplicationDbContext>>();
                IEntityValidator<Comment> entityValidator = serviceProvider.GetService<IEntityValidator<Comment>>();
                return new CommentRepository(applicationDbFactory.Create(), entityValidator);
            });
            #endregion Repositories

            #region Services
            serviceCollection.AddTransient<IExternalMemeFetcherService, ExternalMemeFetcherService>();
            serviceCollection.AddTransient<ILocalMemeFetcherService, LocalMemeFetcherService>();
            serviceCollection.AddTransient<IMemeService, MemeService>(serviceProvider =>
            {
                IRepository<Meme> memeRepository = serviceProvider.GetService<IRepository<Meme>>();
                ILocalMemeFetcherService memeFetcherService = serviceProvider.GetService<ILocalMemeFetcherService>();
                return new MemeService(memeFetcherService, memeRepository);
            });
            serviceCollection.AddTransient<ICollectionService, CollectionService>(serviceProvider =>
            {
                IRepository<Collection> collectionRepository = serviceProvider.GetService<IRepository<Collection>>();
                return new CollectionService(collectionRepository);
            });
            serviceCollection.AddTransient<IUserService, UserService>(serviceProvider =>
            {
                IRepository<ApplicationUser> userRepository = serviceProvider.GetService<IRepository<ApplicationUser>>();
                IRepository<Follower> followerRepository = serviceProvider.GetService<IRepository<Follower>>();
                UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                SignInManager<ApplicationUser> signInManager = serviceProvider.GetService<SignInManager<ApplicationUser>>();
                return new UserService(signInManager, userManager, userRepository, followerRepository);
            });
            serviceCollection.AddTransient<IFollowerService, FollowerService>(serviceProvider =>
            {
                IRepository<Follower> followerRepository = serviceProvider.GetService<IRepository<Follower>>();
                IRepository<ApplicationUser> applicationUserRepository = serviceProvider.GetService<IRepository<ApplicationUser>>();
                return new FollowerService(followerRepository, applicationUserRepository);
            });
            serviceCollection.AddTransient<IMemeSharingService, MemeSharingService>(serviceProvider =>
            {
                IRepository<SharedMeme> sharedMemeRepository = serviceProvider.GetService<IRepository<SharedMeme>>();
                IRepository<ApplicationUser> userRepository = serviceProvider.GetService<IRepository<ApplicationUser>>();
                return new MemeSharingService(sharedMemeRepository, userRepository);
            });
            serviceCollection.AddTransient<IMemeLikingService, MemeLikingService>(serviceProvider =>
            {
                IRepository<MemeLike> memeLikeRepository = serviceProvider.GetService<IRepository<MemeLike>>();
                return new MemeLikingService(memeLikeRepository);
            });
            serviceCollection.AddTransient<ICollectionItemDetailService, CollectionItemDetailService>(serviceProvider =>
            {
                IRepository<CollectionItemDetail> collectionItemDetailRepository = serviceProvider.GetService<IRepository<CollectionItemDetail>>();
                IRepository<Collection> collectionRepository = serviceProvider.GetService<IRepository<Collection>>();
                IRepository<Meme> memeRepository = serviceProvider.GetService<IRepository<Meme>>();
                return new CollectionItemDetailService(collectionItemDetailRepository, collectionRepository, memeRepository);
            });
            serviceCollection.AddTransient<ICollectionSubscriberService, CollectionSubscriberService>(serviceProvider =>
            {
                IRepository<CollectionSubscriber> collectionSubscriberRepository = serviceProvider.GetService<IRepository<CollectionSubscriber>>();
                return new CollectionSubscriberService(collectionSubscriberRepository);
            });
            serviceCollection.AddTransient<ITempDataService, TempDataService>(serviceProvider =>
            {
                ICollectionService collectionService = serviceProvider.GetService<ICollectionService>();
                IFollowerService followerService = serviceProvider.GetService<IFollowerService>();
                return new TempDataService(collectionService, followerService);
            });
            serviceCollection.AddTransient<ICommentService, CommentService>(serviceProvider =>
            {
                IRepository<Comment> commentRepository = serviceProvider.GetService<IRepository<Comment>>();
                IRepository<Meme> memeRepository = serviceProvider.GetService<IRepository<Meme>>();
                return new CommentService(commentRepository, memeRepository);
            });
            #endregion Services

            return serviceCollection;
        }
    }
}
