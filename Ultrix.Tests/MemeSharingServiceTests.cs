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
using Ultrix.Tests.Utilities;

namespace Ultrix.Tests
{
    [TestClass]
    public class MemeSharingServiceTests
    {
        public static IEntityValidator<Meme> MemeValidator { get; set; }
        public static ILocalMemeFetcherService LocalMemeFetcherService { get; set; }
        public static IFactory<AppDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<Meme> MemeRepository { get; set; }
        public static IMemeService MemeService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            MemeValidator = new MemeValidator();
            LocalMemeFetcherService = new LocalMemeFetcherService();
        }

        [TestInitialize]
        public async Task InitializeAsync()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();
            MemeRepository = new MemeRepository(ApplicationDbFactory.Create(), MemeValidator);
            MemeService = new MemeService(LocalMemeFetcherService, MemeRepository);
        }

        //[TestMethod]
        //public async Task ShareMemeToMutualFollowerAsync_Should_Fail()
        //{
        //    // Arrange
        //    MemeDto expectedMemeDto = new MemeDto
        //    {
        //        Id = "a0Q558q",
        //        ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
        //        VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
        //        PageUrl = "http://9gag.com/gag/a0Q558q",
        //        Title = "Old but Gold"
        //    };
        //    Meme entity = DtoToEntityConverter.Convert<Meme, MemeDto>(expectedMemeDto);
        //    Assert.IsTrue(await MemeRepository.CreateAsync(entity));
        //    MemeDto actualMemeDto = await MemeService.GetMemeAsync("a0Q558q");
        //    Assert.AreEqual(expectedMemeDto, actualMemeDto);
        //}
    }
}
