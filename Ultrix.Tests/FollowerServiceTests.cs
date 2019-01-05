using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Managers;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.Utilities;

namespace Ultrix.Tests
{
    [TestClass]
    public class FollowerServiceTests
    {
        public static IEntityValidator<ApplicationUser> ApplicationUserValidator { get; set; }
        public static IEntityValidator<Follower> FollowerValidator { get; set; }
        public static IEntityValidator<Credential> CredentialValidator { get; set; }
        public static IEntityValidator<CredentialType> CredentialTypeValidator { get; set; }
        public static IEntityValidator<Role> RoleValidator { get; set; }
        public static IEntityValidator<UserRole> UserRoleValidator { get; set; }
        public static IEntityValidator<RolePermission> RolePermissionValidator { get; set; }
        public static IEntityValidator<Permission> PermissionValidator { get; set; }
        public static IFactory<AppDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<ApplicationUser> ApplicationUserRepository { get; set; }
        public static IRepository<Follower> FollowerRepository { get; set; }
        public static IRepository<Credential> CredentialRepository { get; set; }
        public static IRepository<CredentialType> CredentialTypeRepository { get; set; }
        public static IRepository<Role> RoleRepository { get; set; }
        public static IRepository<UserRole> UserRoleRepository { get; set; }
        public static IRepository<RolePermission> RolePermissionRepository { get; set; }
        public static IRepository<Permission> PermissionRepository { get; set; }
        public static IHttpContextAccessor HttpContextAccessor { get; set; }
        public static IUserService UserService { get; set; }
        public static IUserManager UserManager { get; set; }
        public static IFollowerService FollowerService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            ApplicationUserValidator = new ApplicationUserValidator();
            FollowerValidator = new FollowerValidator();
            CredentialValidator = new CredentialValidator();
            CredentialTypeValidator = new CredentialTypeValidator();
            RoleValidator = new RoleValidator();
            UserRoleValidator = new UserRoleValidator();
            RolePermissionValidator = new RolePermissionValidator();
            PermissionValidator = new PermissionValidator();
        }

        [TestInitialize]
        public async Task InitializeAsync()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();

            ApplicationUserRepository = new UserRepository(ApplicationDbFactory.Create(), ApplicationUserValidator);
            FollowerRepository = new FollowerRepository(ApplicationDbFactory.Create(), FollowerValidator);
            CredentialRepository = new CredentialRepository(ApplicationDbFactory.Create(), CredentialValidator);
            CredentialTypeRepository = new CredentialTypeRepository(ApplicationDbFactory.Create(), CredentialTypeValidator);
            RoleRepository = new RoleRepository(ApplicationDbFactory.Create(), RoleValidator);
            UserRoleRepository = new UserRoleRepository(ApplicationDbFactory.Create(), UserRoleValidator);
            RolePermissionRepository = new RolePermissionRepository(ApplicationDbFactory.Create(), RolePermissionValidator);
            PermissionRepository = new PermissionRepository(ApplicationDbFactory.Create(), PermissionValidator);
            HttpContextAccessor = new HttpContextAccessor(); // NOTE: Don't actually use it, when using Startup it will inject the HttpContext. (here it will always be null)

            UserManager = new UserManager(ApplicationUserRepository, CredentialTypeRepository, CredentialRepository, RoleRepository, UserRoleRepository, RolePermissionRepository, PermissionRepository, HttpContextAccessor);
            UserService = new UserService(UserManager, ApplicationUserRepository, FollowerRepository);
            FollowerService = new FollowerService(FollowerRepository, ApplicationUserRepository);

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

            await UserService.RegisterUserAsync(new RegisterUserDto
            {
                Email = "Bob.Keizer@Ultrix.nl",
                Password = "password",
                UserName = "Bobbie"
            });

            await UserService.RegisterUserAsync(new RegisterUserDto
            {
                Email = "Karel.Kerel@Ultrix.nl",
                Password = "password",
                UserName = "Karel"
            });
        }

        [TestMethod]
        public async Task FollowUserAsync_Should_Pass()
        {
            // Arrange
            FollowerDto followerDto = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 2
            };

            // Act
            ServiceResponseDto responseDto = await FollowerService.FollowUserAsync(followerDto);

            // Assert
            Assert.IsTrue(responseDto.Success);
        }

        [TestMethod]
        public async Task FollowUserAsync_Should_Fail_User_Does_Not_Exist()
        {
            // Arrange
            FollowerDto followerDto = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 6
            };

            // Act
            ServiceResponseDto responseDto = await FollowerService.FollowUserAsync(followerDto);

            // Assert
            Assert.IsFalse(responseDto.Success);
        }

        [TestMethod]
        public async Task UnFollowUserAsync_Should_Pass()
        {
            // Arrange
            FollowerDto followerDto = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 2
            };
            await FollowerService.FollowUserAsync(followerDto);

            // Act
            ServiceResponseDto responseDto = await FollowerService.UnFollowUserAsync(followerDto);

            // Assert
            Assert.IsTrue(responseDto.Success);
        }

        [TestMethod]
        public async Task UnFollowUserAsync_Should_Fail_Because_User_Is_Not_Currently_Followed()
        {
            // Arrange
            FollowerDto followerDto = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 2
            };

            // Act
            ServiceResponseDto responseDto = await FollowerService.UnFollowUserAsync(followerDto);

            // Assert
            Assert.IsFalse(responseDto.Success);
        }

        [TestMethod]
        public async Task GetFollowersByUserIdAsync_Should_Pass()
        {
            // Arrange
            FollowerDto followerDto1 = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 2
            };
            FollowerDto followerDto2 = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 3
            };
            FollowerDto followerDto3 = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 4
            };
            await FollowerService.FollowUserAsync(followerDto1);
            await FollowerService.FollowUserAsync(followerDto2);
            await FollowerService.FollowUserAsync(followerDto3);

            // Act
            List<Follower> followers = (await FollowerRepository.FindManyByExpressionAsync(follower => follower.UserId.Equals(1))).ToList();

            // Assert
            Assert.IsTrue(followers.All(follower => follower.FollowerUserId.Equals(2) || follower.FollowerUserId.Equals(3) || follower.FollowerUserId.Equals(4)));
        }

        [TestMethod]
        public async Task GetFollowsByUserIdAsync_Should_Pass()
        {
            // Arrange
            FollowerDto followerDto1 = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 2
            };
            FollowerDto followerDto2 = new FollowerDto
            {
                UserId = 3,
                FollowerUserId = 2
            };
            FollowerDto followerDto3 = new FollowerDto
            {
                UserId = 4,
                FollowerUserId = 2
            };
            await FollowerService.FollowUserAsync(followerDto1);
            await FollowerService.FollowUserAsync(followerDto2);
            await FollowerService.FollowUserAsync(followerDto3);

            // Act
            List<Follower> follows = (await FollowerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(2))).ToList();

            // Assert
            Assert.IsTrue(follows.All(follower => follower.UserId.Equals(1) || follower.UserId.Equals(3) || follower.UserId.Equals(4)));
        }

        [TestMethod]
        public async Task GetMutualFollowersByUserIdAsync_Should_Pass()
        {
            // Arrange
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
            FollowerDto followerDto3 = new FollowerDto
            {
                UserId = 4,
                FollowerUserId = 1
            };
            FollowerDto followerDto4 = new FollowerDto
            {
                UserId = 1,
                FollowerUserId = 4
            };
            await FollowerService.FollowUserAsync(followerDto1);
            await FollowerService.FollowUserAsync(followerDto2);
            await FollowerService.FollowUserAsync(followerDto3);
            await FollowerService.FollowUserAsync(followerDto4);

            // Act
            List<Follower> follows = (await FollowerRepository.FindManyByExpressionAsync(follower => follower.FollowerUserId.Equals(1))).ToList();
            List<Follower> followers = (await FollowerRepository.FindManyByExpressionAsync(follower => follower.UserId.Equals(1))).ToList();
            List<bool> mutualFollows = followers.Select(follower => follows.Any(following => following.UserId.Equals(follower.FollowerUserId))).ToList();

            // Assert
            Assert.AreEqual(mutualFollows.Count, 2);
        }
    }
}
