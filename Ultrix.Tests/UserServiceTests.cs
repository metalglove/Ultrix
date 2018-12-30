using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;

namespace Ultrix.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        public static IEntityValidator<ApplicationUser> ApplicationUserValidator { get; set; }
        public static IEntityValidator<Follower> FollowerValidator { get; set; }
        public static IFactory<ApplicationDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<ApplicationUser> ApplicationUserRepository { get; set; }
        public static IRepository<Follower> FollowerRepository { get; set; }
        public static IUserService UserService { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            ApplicationUserValidator = new ApplicationUserValidator();
            FollowerValidator = new FollowerValidator();
        }

        // TODO: To be able to test the UserService the UserManager needs to be reimplemented using this link:
        // https://docs.microsoft.com/en-us/aspnet/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity#userclass

        [TestInitialize]
        public void Initialize()
        {
            //ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            //ApplicationUserRepository = new UserRepository(ApplicationDbFactory.Create(), ApplicationUserValidator);
            //FollowerRepository = new FollowerRepository(ApplicationDbFactory.Create(), FollowerValidator);
            //UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
            //    new UserStore<ApplicationUser>(ApplicationDbFactory.Create()));
            
            //UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>();
            //SignInManager<ApplicationUser> signInManager = new SignInManager<ApplicationUser>(userManager,);

            //UserService = new UserService(signInManager, userManager, ApplicationUserRepository, FollowerRepository);
        }
    }
}
