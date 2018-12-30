using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Extensions;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Tests
{
    [TestClass]
    public class CollectionItemDetailServiceTests
    {
        public static IEntityValidator<Collection> CollectionValidator { get; set; }
        public static IEntityValidator<Meme> MemeValidator { get; set; }
        public static IEntityValidator<CollectionItemDetail> CollectionItemDetailValidator { get; set; }
        public static IFactory<ApplicationDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<Collection> CollectionRepository { get; set; }
        public static IRepository<Meme> MemeRepository { get; set; }
        public static IRepository<CollectionItemDetail> CollectionItemDetailRepository { get; set; }
        public static ICollectionService CollectionService { get; set; }
        public static ICollectionItemDetailService CollectionItemDetailService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            CollectionValidator = new CollectionValidator();
            MemeValidator = new MemeValidator();
            CollectionItemDetailValidator = new CollectionItemDetailValidator();
        }

        [TestInitialize]
        public async Task Initialize()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();
            // Relational-specific methods can only be used when the context is using a relational database provider..
            // ApplicationDbFactory.Create().Database.Migrate();
            CollectionRepository = new CollectionRepository(ApplicationDbFactory.Create(), CollectionValidator);
            MemeRepository = new MemeRepository(ApplicationDbFactory.Create(), MemeValidator);
            CollectionItemDetailRepository = new CollectionItemDetailRepository(ApplicationDbFactory.Create(), CollectionItemDetailValidator);
            CollectionService = new CollectionService(CollectionRepository);
            CollectionItemDetailService = new CollectionItemDetailService(CollectionItemDetailRepository, CollectionRepository, MemeRepository);
            CollectionDto collectionDto = new CollectionDto
            {
                UserId = 1,
                Name = "Dank Memes"
            };
            ServiceResponseDto createCollectionResultDto = await CollectionService.CreateCollectionAsync(collectionDto);
            Assert.IsTrue(createCollectionResultDto.Success);
        }

        [TestMethod]
        public async Task AddMemeToCollectionAsync_Should_Fail_With_Message_Meme_Does_Not_Exist()
        {
            // Arrange
            AddMemeToCollectionDto addMemeToCollectionDto = new AddMemeToCollectionDto
            {
                MemeId = "a0Q558q",
                UserId = 1,
                CollectionId = 1
            };

            // Act
            ServiceResponseDto serviceResultDto = await CollectionItemDetailService.AddMemeToCollectionAsync(addMemeToCollectionDto);
            
            // Assert
            Assert.IsFalse(serviceResultDto.Success);
            Assert.AreEqual(serviceResultDto.Message, "Meme does not exist.");
        }

        [TestMethod]
        public async Task AddMemeToCollectionAsync_Should_Pass()
        {
            // Arrange
            MemeDto expectedMemeDto = new MemeDto
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold"
            };
            Meme entity = DtoToEntityConverter.Convert<Meme, MemeDto>(expectedMemeDto);
            Assert.IsTrue(await MemeRepository.CreateAsync(entity));
            AddMemeToCollectionDto addMemeToCollectionDto = new AddMemeToCollectionDto
            {
                MemeId = "a0Q558q",
                UserId = 1,
                CollectionId = 1
            };

            // Act
            ServiceResponseDto addMemeToCollectionResultDto = await CollectionItemDetailService.AddMemeToCollectionAsync(addMemeToCollectionDto);
            
            // Assert
            Assert.IsTrue(addMemeToCollectionResultDto.Success);
            Assert.AreEqual(addMemeToCollectionResultDto.Message, "Succesfully added meme to the collection.");
            CollectionItemDetail actualCollectionItemDetail = (await CollectionItemDetailRepository.GetAllAsync()).First();
            Assert.IsTrue(
                actualCollectionItemDetail.MemeId.Equals("a0Q558q") && 
                actualCollectionItemDetail.AddedByUserId.Equals(1) && 
                actualCollectionItemDetail.CollectionId.Equals(1));
        }

        [TestMethod]
        public async Task RemoveMemeFromCollectionAsync_Should_Pass()
        {
            // Arrange
            MemeDto expectedMemeDto = new MemeDto
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold"
            };
            Meme entity = DtoToEntityConverter.Convert<Meme, MemeDto>(expectedMemeDto);
            Assert.IsTrue(await MemeRepository.CreateAsync(entity));
            AddMemeToCollectionDto addMemeToCollectionDto = new AddMemeToCollectionDto
            {
                MemeId = "a0Q558q",
                UserId = 1,
                CollectionId = 1
            };
            ServiceResponseDto addMemeToCollectionResultDto = await CollectionItemDetailService.AddMemeToCollectionAsync(addMemeToCollectionDto);
            Assert.IsTrue(addMemeToCollectionResultDto.Success);
            RemoveMemeFromCollectionDto removeMemeFromCollectionDto = new RemoveMemeFromCollectionDto
            {
                CollectionId = 1,
                CollectionItemDetailId = 1,
                UserId = 1
            };

            // Act
            bool success = await CollectionItemDetailService.RemoveMemeFromCollectionAsync(removeMemeFromCollectionDto);

            // Assert
            Assert.IsTrue(success);
            List<CollectionItemDetail> collectionItemDetails = (await CollectionItemDetailRepository.GetAllAsync()).ToList();
            Assert.AreEqual(collectionItemDetails.Count(), 0);
        }

        [TestMethod]
        public async Task RemoveMemeFromCollectionAsync_Should_Throw_ApplicationUserIsNotAuthorizedException()
        {
            // Arrange
            MemeDto expectedMemeDto = new MemeDto
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold"
            };
            Meme entity = DtoToEntityConverter.Convert<Meme, MemeDto>(expectedMemeDto);
            Assert.IsTrue(await MemeRepository.CreateAsync(entity));
            AddMemeToCollectionDto addMemeToCollectionDto = new AddMemeToCollectionDto
            {
                MemeId = "a0Q558q",
                UserId = 1,
                CollectionId = 1
            };
            ServiceResponseDto addMemeToCollectionResultDto = await CollectionItemDetailService.AddMemeToCollectionAsync(addMemeToCollectionDto);
            Assert.IsTrue(addMemeToCollectionResultDto.Success);

            
            RemoveMemeFromCollectionDto removeMemeFromCollectionDto = new RemoveMemeFromCollectionDto
            {
                CollectionId = 1,
                CollectionItemDetailId = 1,
                UserId = 2
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ApplicationUserIsNotAuthorizedException>(
                async () => await CollectionItemDetailService.RemoveMemeFromCollectionAsync(removeMemeFromCollectionDto));
        }
    }
}
