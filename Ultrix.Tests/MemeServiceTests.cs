using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Infrastructure.Services;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Tests
{
    [TestClass]
    public class MemeServiceTests
    {
        public static IEntityValidator<Meme> MemeValidator { get; set; }
        public static ILocalMemeFetcherService LocalMemeFetcherService { get; set; }
        public static IFactory<ApplicationDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<Meme> MemeRepository { get; set; }
        public static IMemeService MemeService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            MemeValidator = new MemeValidator();
            LocalMemeFetcherService = new LocalMemeFetcherService();
        }

        [TestInitialize]
        public void Initialize()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            MemeRepository = new MemeRepository(ApplicationDbFactory.Create(), MemeValidator);
            MemeService = new MemeService(LocalMemeFetcherService, MemeRepository);
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
            await MemeRepository.CreateAsync(entity);

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
        public async Task GetRandomMemeAync_Should_Return_2_Unique_Memes()
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
