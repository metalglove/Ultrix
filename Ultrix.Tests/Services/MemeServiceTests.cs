using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Exceptions;
using Ultrix.Infrastructure.Services;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.TestUtilities;

namespace Ultrix.Tests.Services
{
    [TestClass]
    public class MemeServiceTests : ServiceTestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            MemeValidator = new MemeValidator();
            MemeFetcherService = new LocalMemeFetcherService();
        }

        [TestInitialize]
        public async Task InitializeAsync()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();
            MemeRepository = new MemeRepository(ApplicationDbFactory.Create(), MemeValidator);
            MemeService = new MemeService(MemeFetcherService, MemeRepository);
        }

        [TestMethod]
        public async Task GetMemeAsync_With_Id_a0Q558q_Should_Return_MemeAsync()
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

            // Act
            MemeDto actualMemeDto = await MemeService.GetMemeAsync("a0Q558q");

            // Assert
            Assert.AreEqual(expectedMemeDto, actualMemeDto);
        }

        [TestMethod]
        public async Task GetMemeAsync_With_Id_a0Q558q_With_Empty_Repository_Should_Throw_EntityNotFoundException()
        {
            // Arrange 
            // in TestInitialize

            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await MemeService.GetMemeAsync("a0Q558q"));
        }

        [TestMethod]
        public async Task GetRandomMemeAsync_Should_Return_2_Unique_Memes()
        {
            // Arrange 
            // in TestInitialize

            // Act
            MemeDto memeDto1 = await MemeService.GetRandomMemeAsync();
            MemeDto memeDto2 = await MemeService.GetRandomMemeAsync();

            // Assert
            Assert.AreNotEqual(memeDto1, memeDto2);
        }
    }
}
