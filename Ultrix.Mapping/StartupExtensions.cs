using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ultrix.Application.Cryptography;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Managers;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
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
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<IFactory<AppDbContext>, ApplicationDbFactory>();

            serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    }
                );

            #region Cryptography
            serviceCollection.AddSingleton<IHasher, Pbkdf2Hasher>();
            serviceCollection.AddSingleton<ISaltGenerator, RandomSaltGenerator>();
            #endregion Cryptography

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
            serviceCollection.AddTransient<IEntityValidator<Credential>, CredentialValidator>();
            serviceCollection.AddTransient<IEntityValidator<CredentialType>, CredentialTypeValidator>();
            serviceCollection.AddTransient<IEntityValidator<Role>, RoleValidator>();
            serviceCollection.AddTransient<IEntityValidator<UserRole>, UserRoleValidator>();
            serviceCollection.AddTransient<IEntityValidator<RolePermission>, RolePermissionValidator>();
            serviceCollection.AddTransient<IEntityValidator<Permission>, PermissionValidator>();
            #endregion Validators

            #region Repositories
            serviceCollection.AddTransient<IRepository<Meme>, MemeRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Meme> entityValidator = serviceProvider.GetService<IEntityValidator<Meme>>();
                return new MemeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Follower>, FollowerRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Follower> entityValidator = serviceProvider.GetService<IEntityValidator<Follower>>();
                return new FollowerRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Collection>, CollectionRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Collection> entityValidator = serviceProvider.GetService<IEntityValidator<Collection>>();
                return new CollectionRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<SharedMeme>, SharedMemeRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<SharedMeme> entityValidator = serviceProvider.GetService<IEntityValidator<SharedMeme>>();
                return new SharedMemeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<ApplicationUser>, UserRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<ApplicationUser> entityValidator = serviceProvider.GetService<IEntityValidator<ApplicationUser>>();
                return new UserRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<MemeLike>, MemeLikeRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<MemeLike> entityValidator = serviceProvider.GetService<IEntityValidator<MemeLike>>();
                return new MemeLikeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<CollectionItemDetail>, CollectionItemDetailRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<CollectionItemDetail> entityValidator = serviceProvider.GetService<IEntityValidator<CollectionItemDetail>>();
                return new CollectionItemDetailRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<CollectionSubscriber>, CollectionSubscriberRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<CollectionSubscriber> entityValidator = serviceProvider.GetService<IEntityValidator<CollectionSubscriber>>();
                return new CollectionSubscriberRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Comment>, CommentRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Comment> entityValidator = serviceProvider.GetService<IEntityValidator<Comment>>();
                return new CommentRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Credential>, CredentialRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Credential> entityValidator = serviceProvider.GetService<IEntityValidator<Credential>>();
                return new CredentialRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<CredentialType>, CredentialTypeRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<CredentialType> entityValidator = serviceProvider.GetService<IEntityValidator<CredentialType>>();
                return new CredentialTypeRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Role>, RoleRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Role> entityValidator = serviceProvider.GetService<IEntityValidator<Role>>();
                return new RoleRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<UserRole>, UserRoleRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<UserRole> entityValidator = serviceProvider.GetService<IEntityValidator<UserRole>>();
                return new UserRoleRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<RolePermission>, RolePermissionRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<RolePermission> entityValidator = serviceProvider.GetService<IEntityValidator<RolePermission>>();
                return new RolePermissionRepository(applicationDbFactory.Create(), entityValidator);
            });
            serviceCollection.AddTransient<IRepository<Permission>, PermissionRepository>(serviceProvider =>
            {
                IFactory<AppDbContext> applicationDbFactory = serviceProvider.GetService<IFactory<AppDbContext>>();
                IEntityValidator<Permission> entityValidator = serviceProvider.GetService<IEntityValidator<Permission>>();
                return new PermissionRepository(applicationDbFactory.Create(), entityValidator);
            });
            #endregion Repositories

            #region Managers
            serviceCollection.AddScoped<IUserManager, UserManager>(serviceProvider =>
            {
                IRepository<ApplicationUser> applicationUserRepository = serviceProvider.GetService<IRepository<ApplicationUser>>();
                IRepository<Credential> credentialRepository = serviceProvider.GetService<IRepository<Credential>>();
                IRepository<CredentialType> credentialTypeRepository = serviceProvider.GetService<IRepository<CredentialType>>();
                IRepository<Role> roleRepository = serviceProvider.GetService<IRepository<Role>>();
                IRepository<UserRole> userRoleRepository = serviceProvider.GetService<IRepository<UserRole>>();
                IRepository<RolePermission> rolePermissionRepository = serviceProvider.GetService<IRepository<RolePermission>>();
                IRepository<Permission> permissionRepository = serviceProvider.GetService<IRepository<Permission>>();
                IHttpContextAccessor httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                IHasher hasher = serviceProvider.GetService<IHasher>();
                ISaltGenerator saltGenerator = serviceProvider.GetService<ISaltGenerator>();
                return new UserManager(applicationUserRepository, credentialTypeRepository, credentialRepository, roleRepository, userRoleRepository, rolePermissionRepository, permissionRepository, httpContextAccessor, hasher, saltGenerator);
            });
            #endregion Managers

            #region Services
            //serviceCollection.AddTransient<IExternalMemeFetcherService, ExternalMemeFetcherService>();
            serviceCollection.AddTransient<IMemeFetcherService, LocalMemeFetcherService>();
            serviceCollection.AddTransient<IMemeService, MemeService>(serviceProvider =>
            {
                IRepository<Meme> memeRepository = serviceProvider.GetService<IRepository<Meme>>();
                IMemeFetcherService memeFetcherService = serviceProvider.GetService<IMemeFetcherService>();
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
                IUserManager userManager = serviceProvider.GetService<IUserManager>();
                return new UserService(userManager, userRepository, followerRepository);
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
