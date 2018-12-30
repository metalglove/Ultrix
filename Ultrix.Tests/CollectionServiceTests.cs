using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.Utilities;

namespace Ultrix.Tests
{
    [TestClass]
    public class CollectionServiceTests
    {
        public static IEntityValidator<Collection> CollectionValidator { get; set; }
        public static IFactory<ApplicationDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<Collection> CollectionRepository { get; set; }
        public static ICollectionService CollectionService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            CollectionValidator = new CollectionValidator();
        }

        [TestInitialize]
        public async Task Initialize()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();
            CollectionRepository = new CollectionRepository(ApplicationDbFactory.Create(), CollectionValidator);
            CollectionService = new CollectionService(CollectionRepository);
        }

        [TestMethod]
        public async Task GetAllCollectionsAsync_Should_Return_3_Collections()
        {
            // Arrange
            CollectionDto expectedCollectionDto1 = new CollectionDto
            {
                UserId = 1,
                Name = "Dank Memes1"
            };
            Collection entity1 = DtoToEntityConverter.Convert<Collection, CollectionDto>(expectedCollectionDto1);
            await CollectionRepository.CreateAsync(entity1);
            CollectionDto expectedCollectionDto2 = new CollectionDto
            {
                UserId = 1,
                Name = "Dank Memes2"
            };
            Collection entity2 = DtoToEntityConverter.Convert<Collection, CollectionDto>(expectedCollectionDto2);
            await CollectionRepository.CreateAsync(entity2);
            CollectionDto expectedCollectionDto3 = new CollectionDto
            {
                UserId = 1,
                Name = "Dank Memes3"
            };
            Collection entity3 = DtoToEntityConverter.Convert<Collection, CollectionDto>(expectedCollectionDto3);
            await CollectionRepository.CreateAsync(entity3);

            // Act
            CollectionDto actualCollectionDto1 = (await CollectionService.GetAllCollectionsAsync()).First();
            CollectionDto actualCollectionDto2 = (await CollectionService.GetAllCollectionsAsync()).Skip(1).First();
            CollectionDto actualCollectionDto3 = (await CollectionService.GetAllCollectionsAsync()).Skip(2).First();

            // Assert
            Assert.AreEqual(expectedCollectionDto1, actualCollectionDto1);
            Assert.AreEqual(expectedCollectionDto2, actualCollectionDto2);
            Assert.AreEqual(expectedCollectionDto3, actualCollectionDto3);
        }

        [TestMethod]
        public async Task CreateCollectionAsync_Should_Pass()
        {
            // Arrange
            CollectionDto expectedCollectionDto1 = new CollectionDto
            {
                UserId = 1,
                Name = "Dank"
            };
            ServiceResponseDto createCollectionResultDto = await CollectionService.CreateCollectionAsync(expectedCollectionDto1);
            Assert.IsTrue(createCollectionResultDto.Success);

            // Act
            CollectionDto actualCollectionDto1 = await CollectionService.GetCollectionByIdAsync(1);

            // Assert
            Assert.AreEqual(expectedCollectionDto1, actualCollectionDto1);
        }

        [TestMethod]
        public async Task DeleteCollectionAsync_Should_Pass()
        {
            // Arrange
            CollectionDto collectionDto = new CollectionDto
            {
                UserId = 1,
                Name = "Dank Memes"
            };
            await CollectionService.CreateCollectionAsync(collectionDto);
            List<CollectionDto> collections = (await CollectionService.GetAllCollectionsAsync()).ToList();
            Assert.IsTrue(collections.Count.Equals(1));

            // Act
            DeleteCollectionDto deleteCollectionDto = new DeleteCollectionDto { Id = 1, UserId = 1 };
            ServiceResponseDto deleteCollectionResultDto = await CollectionService.DeleteCollectionAsync(deleteCollectionDto);

            // Assert
            Assert.IsTrue(deleteCollectionResultDto.Success);
            collections = (await CollectionService.GetAllCollectionsAsync()).ToList();
            Assert.IsTrue(collections.Count.Equals(0));
        }
    }
}
