using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Managers;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Domain.Enumerations;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.Utilities;

namespace Ultrix.Tests
{
    [TestClass]
    public class UserServiceTests
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
        public async Task Initialize()
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

            // A Credential type is required for a user to be able to login or register.
            await CredentialTypeRepository.CreateAsync(new CredentialType
            {
                Code = "Email",
                Name = "Email",
                Position = 1
            });
        }

        [TestMethod]
        public async Task RegisterUserAsync_Should_Pass()
        {
            // Arrange
            RegisterUserDto registerUserDto = new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password",
                UserName = "Metalglove"
            };

            // Act
            SignUpResultDto signUpResultDto = await UserService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.IsTrue(signUpResultDto.Success);
        }

        [TestMethod]
        public async Task RegisterUserAsync_Should_Fail_Because_Of_Empty_Password()
        {
            // Arrange
            RegisterUserDto registerUserDto = new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "",
                UserName = "Metalglove"
            };

            // Act
            SignUpResultDto signUpResultDto = await UserService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.IsFalse(signUpResultDto.Success);
            Assert.AreEqual(signUpResultDto.Error, SignUpResultError.SecretIsNullOrEmpty);
        }

        [TestMethod]
        public async Task SignInAsync_Should_Pass()
        {
            // Arrange
            RegisterUserDto registerUserDto = new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password",
                UserName = "Metalglove"
            };
            SignUpResultDto signUpResultDto = await UserService.RegisterUserAsync(registerUserDto);
            Assert.IsTrue(signUpResultDto.Success);
            LoginUserDto loginUserDto = new LoginUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password"
            };

            // Act
            // The UserManager is used here instead of UserService so it skips the actual SignIn into the HttpContext
            ValidateResultDto validateResultDto = await UserManager.ValidateAsync("Email", loginUserDto.Email, loginUserDto.Password);

            // Assert
            Assert.IsTrue(validateResultDto.Success);
        }

        [TestMethod]
        public async Task SignInAsync_Should_Fail_Using_Wrong_Password()
        {
            // Arrange
            RegisterUserDto registerUserDto = new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password",
                UserName = "Metalglove"
            };
            SignUpResultDto signUpResultDto = await UserService.RegisterUserAsync(registerUserDto);
            Assert.IsTrue(signUpResultDto.Success);
            LoginUserDto loginUserDto = new LoginUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password2"
            };

            // Act
            // NOTE: The UserManager is used here instead of UserService so it skips the actual SignIn into the HttpContext
            ValidateResultDto validateResultDto = await UserManager.ValidateAsync("Email", loginUserDto.Email, loginUserDto.Password);

            // Assert
            Assert.IsFalse(validateResultDto.Success);
        }
    }
}
