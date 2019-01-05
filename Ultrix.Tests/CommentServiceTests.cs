﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Application.Managers;
using Ultrix.Application.Services;
using Ultrix.Application.Validators;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Contexts;
using Ultrix.Persistance.Infrastructure;
using Ultrix.Persistance.Repositories;
using Ultrix.Tests.Utilities;

namespace Ultrix.Tests
{
    [TestClass]
    public class CommentServiceTests
    {
        public static IEntityValidator<ApplicationUser> ApplicationUserValidator { get; set; }
        public static IEntityValidator<Follower> FollowerValidator { get; set; }
        public static IEntityValidator<Credential> CredentialValidator { get; set; }
        public static IEntityValidator<CredentialType> CredentialTypeValidator { get; set; }
        public static IEntityValidator<Role> RoleValidator { get; set; }
        public static IEntityValidator<UserRole> UserRoleValidator { get; set; }
        public static IEntityValidator<RolePermission> RolePermissionValidator { get; set; }
        public static IEntityValidator<Permission> PermissionValidator { get; set; }
        public static IEntityValidator<Meme> MemeValidator { get; set; }
        public static IEntityValidator<Comment> CommentValidator { get; set; }
        public static IFactory<AppDbContext> ApplicationDbFactory { get; set; }
        public static IRepository<ApplicationUser> ApplicationUserRepository { get; set; }
        public static IRepository<Meme> MemeRepository { get; set; }
        public static IRepository<Comment> CommentRepository { get; set; }
        public static IRepository<Follower> FollowerRepository { get; set; }
        public static IRepository<Credential> CredentialRepository { get; set; }
        public static IRepository<CredentialType> CredentialTypeRepository { get; set; }
        public static IRepository<Role> RoleRepository { get; set; }
        public static IRepository<UserRole> UserRoleRepository { get; set; }
        public static IRepository<RolePermission> RolePermissionRepository { get; set; }
        public static IRepository<Permission> PermissionRepository { get; set; }
        public static IHttpContextAccessor HttpContextAccessor { get; set; }
        public static IUserService UserService { get; set; }
        public static ICommentService CommentService { get; set; }
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
            MemeValidator = new MemeValidator();
            CommentValidator = new CommentValidator();
        }

        [TestInitialize]
        public async Task Initialize()
        {
            ApplicationDbFactory = new ApplicationDbFactory("InMemoryDatabase");
            await ApplicationDbFactory.Create().Database.EnsureDeletedAsync();
            await ApplicationDbFactory.Create().Database.EnsureCreatedAsync();
            ApplicationDbFactory.Create().ResetValueGenerators();

            MemeRepository = new MemeRepository(ApplicationDbFactory.Create(), MemeValidator);
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

            UserManager = new UserManager(ApplicationUserRepository, CredentialTypeRepository, CredentialRepository, RoleRepository, UserRoleRepository, RolePermissionRepository, PermissionRepository, HttpContextAccessor);
            UserService = new UserService(UserManager, ApplicationUserRepository, FollowerRepository);
            CommentService = new CommentService(CommentRepository, MemeRepository);

            // A Credential type is required for a user to be able to login or register.
            await CredentialTypeRepository.CreateAsync(new CredentialType
            {
                Code = "Email",
                Name = "Email",
                Position = 1
            });

            await UserService.RegisterUserAsync(new RegisterUserDto
            {
                Email = "Mario.Mario@Ultrix.nl",
                Password = "password",
                UserName = "Metalglove"
            });

            Meme meme = new Meme
            {
                Id = "a0Q558q",
                ImageUrl = "https://images-cdn.9gag.com/photo/a0Q558q_700b.jpg",
                VideoUrl = "http://img-9gag-fun.9cache.com/photo/a0Q558q_460sv.mp4",
                PageUrl = "http://9gag.com/gag/a0Q558q",
                Title = "Old but Gold"
            };
            await MemeRepository.CreateAsync(meme);
        }

        [TestMethod]
        public async Task CreateCommentAsync_Should_Pass()
        {
            // Arrange
            CommentDto commentDto = new CommentDto
            {
                MemeId = "a0Q558q",
                Text = "Haha, super funny meme!",
                UserId = 1
            };
            // NOTE: the repository is used here instead of CommentService.GetCommentsByMemeIdAsync because relational data will not work in memory.
            List<Comment> commentsBefore = (List<Comment>) await CommentRepository.FindManyByExpressionAsync(comment => comment.MemeId.Equals("a0Q558q"));
            Assert.IsTrue(commentsBefore.Count.Equals(0));

            // Act
            ServiceResponseDto responseDto = await CommentService.CreateCommentAsync(commentDto);

            // Assert
            Assert.IsTrue(responseDto.Success);
            List<Comment> commentsAfter = (List<Comment>)await CommentRepository.FindManyByExpressionAsync(comment => comment.MemeId.Equals("a0Q558q"));
            Assert.IsTrue(commentsAfter.Count.Equals(1));
        }

        [TestMethod]
        public async Task CreateCommentAsync_Should_Throw_EntityValidationException_Because_Text_Is_Too_Short()
        {
            // Arrange
            CommentDto commentDto = new CommentDto
            {
                MemeId = "a0Q558q",
                Text = "Too short",
                UserId = 1
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityValidationException>(async () => await CommentService.CreateCommentAsync(commentDto));
        }
    }
}
