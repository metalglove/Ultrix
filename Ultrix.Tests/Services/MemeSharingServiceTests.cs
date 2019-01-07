using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ultrix.Application.DTOs;
using Ultrix.Application.Managers;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Infrastructure.Services;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.TestUtilities;
using System.Collections.Generic;
using Ultrix.Application.Cryptography;

namespace Ultrix.Tests.Services
{
    [TestClass]
    public class MemeSharingServiceTests : ServiceTestsBase
    {
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
            MemeFetcherService = new LocalMemeFetcherService();
            SharedMemeValidator = new SharedMemeValidator();
            CommentValidator = new CommentValidator();
            Hasher = new Pbkdf2Hasher();
            SaltGenerator = new RandomSaltGenerator();
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

            UserManager = new UserManager(ApplicationUserRepository, CredentialTypeRepository, CredentialRepository, RoleRepository, UserRoleRepository, RolePermissionRepository, PermissionRepository, HttpContextAccessor, Hasher, SaltGenerator);
            UserService = new UserService(UserManager, ApplicationUserRepository, FollowerRepository);
            FollowerService = new FollowerService(FollowerRepository, ApplicationUserRepository);
            MemeSharingService = new MemeSharingService(SharedMemeRepository, ApplicationUserRepository);

            // NOTE: A CredentialType is required for a user to be able to login or register.
            await CredentialTypeRepository.CreateAsync(new CredentialType
            {
                Code = "Email",
                Name = "Email",
                Position = 1
            });

            await UserService.SignUpAsync(new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password",
                UserName = "Metalglove"
            });

            await UserService.SignUpAsync(new RegisterUserDto
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
