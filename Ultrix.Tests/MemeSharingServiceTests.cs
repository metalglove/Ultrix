﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ultrix.Application.DTOs;
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
using Ultrix.Tests.Utilities;
using System.Collections.Generic;

namespace Ultrix.Tests
{
    [TestClass]
    public class MemeSharingServiceTests
    {
        public static IEntityValidator<ApplicationUser> ApplicationUserValidator { get; set; }
        public static IEntityValidator<Follower> FollowerValidator { get; set; }
        public static IEntityValidator<Credential> CredentialValidator { get; set; }
        public static IEntityValidator<CredentialType> CredentialTypeValidator { get; set; }
        public static IEntityValidator<Role> RoleValidator { get; set; }
        public static IEntityValidator<UserRole> UserRoleValidator { get; set; }
        public static IEntityValidator<RolePermission> RolePermissionValidator { get; set; }
        public static IEntityValidator<Permission> PermissionValidator { get; set; }
        public static IEntityValidator<Meme> MemeValidator { get; set; }
        public static IEntityValidator<Comment> CommentValidator { get; set; }
        public static IEntityValidator<SharedMeme> SharedMemeValidator { get; set; }
        public static IFactory<AppDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<ApplicationUser> ApplicationUserRepository { get; set; }
        public static IRepository<Meme> MemeRepository { get; set; }
        public static IRepository<Comment> CommentRepository { get; set; }
        public static IRepository<Follower> FollowerRepository { get; set; }
        public static IRepository<Credential> CredentialRepository { get; set; }
        public static IRepository<CredentialType> CredentialTypeRepository { get; set; }
        public static IRepository<Role> RoleRepository { get; set; }
        public static IRepository<UserRole> UserRoleRepository { get; set; }
        public static IRepository<RolePermission> RolePermissionRepository { get; set; }
        public static IRepository<Permission> PermissionRepository { get; set; }
        public static IRepository<SharedMeme> SharedMemeRepository { get; set; }
        public static IHttpContextAccessor HttpContextAccessor { get; set; }
        public static ILocalMemeFetcherService LocalMemeFetcherService { get; set; }
        public static IUserService UserService { get; set; }
        public static IFollowerService FollowerService { get; set; }
        public static IUserManager UserManager { get; set; }
        public static IMemeSharingService MemeSharingService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            MemeValidator = new MemeValidator();
            ApplicationUserValidator = new ApplicationUserValidator();
            FollowerValidator = new FollowerValidator();
            CredentialValidator = new CredentialValidator();
            CredentialTypeValidator = new CredentialTypeValidator();
            RoleValidator = new RoleValidator();
            UserRoleValidator = new UserRoleValidator();
            RolePermissionValidator = new RolePermissionValidator();
            PermissionValidator = new PermissionValidator();
            LocalMemeFetcherService = new LocalMemeFetcherService();
            SharedMemeValidator = new SharedMemeValidator();
            CommentValidator = new CommentValidator();
        }

        [TestInitialize]
        public async Task InitializeAsync()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();

            MemeRepository = new MemeRepository(ApplicationDbFactory.Create(), MemeValidator);
            SharedMemeRepository = new SharedMemeRepository(ApplicationDbFactory.Create(), SharedMemeValidator);
            ApplicationUserRepository = new UserRepository(ApplicationDbFactory.Create(), ApplicationUserValidator);
            FollowerRepository = new FollowerRepository(ApplicationDbFactory.Create(), FollowerValidator);
            CredentialRepository = new CredentialRepository(ApplicationDbFactory.Create(), CredentialValidator);
            CredentialTypeRepository = new CredentialTypeRepository(ApplicationDbFactory.Create(), CredentialTypeValidator);
            RoleRepository = new RoleRepository(ApplicationDbFactory.Create(), RoleValidator);
            UserRoleRepository = new UserRoleRepository(ApplicationDbFactory.Create(), UserRoleValidator);
            RolePermissionRepository = new RolePermissionRepository(ApplicationDbFactory.Create(), RolePermissionValidator);
            PermissionRepository = new PermissionRepository(ApplicationDbFactory.Create(), PermissionValidator);
            CommentRepository = new CommentRepository(ApplicationDbFactory.Create(), CommentValidator);
            HttpContextAccessor = new HttpContextAccessor(); // NOTE: Don't actually use it, when using Startup it will inject the HttpContext. (here it will always be null)

            UserManager = new UserManager(ApplicationUserRepository, CredentialTypeRepository, CredentialRepository, RoleRepository, UserRoleRepository, RolePermissionRepository, PermissionRepository, HttpContextAccessor);
            UserService = new UserService(UserManager, ApplicationUserRepository, FollowerRepository);
            FollowerService = new FollowerService(FollowerRepository, ApplicationUserRepository);
            MemeSharingService = new MemeSharingService(SharedMemeRepository, ApplicationUserRepository);

            // A Credential type is required for a user to be able to login or register.
            await CredentialTypeRepository.CreateAsync(new CredentialType
            {
                Code = "Email",
                Name = "Email",
                Position = 1
            });

            await UserService.RegisterUserAsync(new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password",
                UserName = "Metalglove"
            });

            await UserService.RegisterUserAsync(new RegisterUserDto
            {
                Email = "Jan.Willem@Ultrix.nl",
                Password = "password",
                UserName = "Jantje"
            });

            FollowerDto followerDto1 = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 2
            };
            FollowerDto followerDto2 = new FollowerDto
            {
                UserId = 2,
                FollowerUserId = 1
            };
            await FollowerService.FollowUserAsync(followerDto1);
            await FollowerService.FollowUserAsync(followerDto2);
            Meme meme1 = new Meme
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold"
            };
            await MemeRepository.CreateAsync(meme1);

            Meme meme2 = new Meme
            {
                Id = "a0QQZoL",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0QQZoL_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0QQZoL_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0QQZoL",
                Title = "Austin was listed for a heart transplant, and the doctor said he would deliver the news a heart was available dressed as Chewbacca."
            };
            await MemeRepository.CreateAsync(meme2);
        }

        [TestMethod]
        public async Task ShareMemeToMutualFollowerAsync_Should_Pass()
        {
            // Arrange
            SharedMemeDto sharedMemeDto = new SharedMemeDto
            {
                SenderUserId = 1,
                ReceiverUserId = 2,
                MemeId = "a0Q558q"
            };

            // Act
            ServiceResponseDto serviceResponseDto = await MemeSharingService.ShareMemeToMutualFollowerAsync(sharedMemeDto);

            // Assert
            Assert.IsTrue(serviceResponseDto.Success);
        }

        [TestMethod]
        public async Task GetSharedMemesAsync_Should_Pass()
        {
            // Arrange
            SharedMemeDto sharedMemeDto1 = new SharedMemeDto
            {
                SenderUserId = 1,
                ReceiverUserId = 2,
                MemeId = "a0Q558q"
            };
            SharedMemeDto sharedMemeDto2 = new SharedMemeDto
            {
                SenderUserId = 1,
                ReceiverUserId = 2,
                MemeId = "a0QQZoL"
            };
            await MemeSharingService.ShareMemeToMutualFollowerAsync(sharedMemeDto1);
            await MemeSharingService.ShareMemeToMutualFollowerAsync(sharedMemeDto2);

            // Act
            List<SharedMeme> sharedMemes = (List<SharedMeme>) await SharedMemeRepository.FindManyByExpressionAsync(sharedMeme => sharedMeme.ReceiverUserId.Equals(2));
            
            // Assert
            Assert.IsTrue(sharedMemes.TrueForAll(sharedMeme => sharedMeme.MemeId.Equals("a0Q558q") || sharedMeme.MemeId.Equals("a0QQZoL")));
        }
    }
}
