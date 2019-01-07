using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ultrix.Application.Cryptography;
using Ultrix.Application.DTOs;
using Ultrix.Application.Managers;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Domain.Enumerations;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.TestUtilities;

namespace Ultrix.Tests.Services
{
    [TestClass]
    public class UserServiceTests : ServiceTestsBase
    {
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
            Hasher = new Pbkdf2Hasher();
            SaltGenerator = new RandomSaltGenerator();
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
            UserManager = new UserManager(ApplicationUserRepository, CredentialTypeRepository, CredentialRepository, RoleRepository, UserRoleRepository, RolePermissionRepository, PermissionRepository, HttpContextAccessor, Hasher, SaltGenerator);
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
            SignUpResultDto signUpResultDto = await UserService.SignUpAsync(registerUserDto);

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
            SignUpResultDto signUpResultDto = await UserService.SignUpAsync(registerUserDto);

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
            SignUpResultDto signUpResultDto = await UserService.SignUpAsync(registerUserDto);
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
            SignUpResultDto signUpResultDto = await UserService.SignUpAsync(registerUserDto);
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
