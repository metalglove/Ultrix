using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;

namespace Ultrix.Tests.Validators
{
    [TestClass]
    public class MemeValidatorTests
    {
        public static IEntityValidator<Meme> MemeValidator { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            MemeValidator = new MemeValidator();
        }

        [TestMethod]
        public void Entity_Meme_Is_Null_Should_Throw_EntityValidationException()
        {
            Meme meme = null;
            Assert.ThrowsException<EntityValidationException>(() => MemeValidator.Validate(meme));
        }
        [TestMethod]
        public void Entity_Meme_Id_Contains_WhiteSpaces_Should_Throw_EntityValidationException()
        {
            Meme meme = new Meme
            {
                Id = "   "
            };
            Assert.ThrowsException<EntityValidationException>(() => MemeValidator.Validate(meme));
        }
        [TestMethod]
        public void Entity_Meme_ImageUrl_Is_Not_A_Valid_Url_Should_Throw_EntityValidationException()
        {
            Meme meme = new Meme
            {
                Id = "a0Q524Z",
                ImageUrl = "ht-tpsds://images-cddddn.9gag.com/photo/a0Q524Z_700b.jpg",
                VideoUrl = "sd",
                PageUrl = "http://9gag.com/gag/a0Q524Z",
                Title = "My heroes"
            };
            Assert.ThrowsException<EntityValidationException>(() => MemeValidator.Validate(meme));
        }
        [TestMethod]
        public void Entity_Meme_With_TimestampAdded_Set_Should_Throw_EntityValidationException()
        {
            Meme meme = new Meme
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold",
                TimestampAdded = DateTime.Now
            };
            Assert.ThrowsException<EntityValidationException>(() => MemeValidator.Validate(meme));
        }
        [TestMethod]
        public void Entity_Meme_With_VideoUrl_Null_Should_Be_Valid()
        {
            Meme meme = new Meme
            {
                Id = "a0Q524Z",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q524Z_700b.jpg",
                VideoUrl = null,
                PageUrl = "http://9gag.com/gag/a0Q524Z",
                Title = "My heroes"
            };
            Assert.IsTrue(MemeValidator.Validate(meme));
        }
        [TestMethod]
        public void Entity_Meme_Should_Be_Valid()
        {
            Meme meme = new Meme
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold"
            };
            Assert.IsTrue(MemeValidator.Validate(meme));
        }
    }
}
