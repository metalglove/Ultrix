using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Tests
{
    [TestClass]
    public class ServiceTestsBase
    {
        public static IHasher Hasher { get; set; }
        public static ISaltGenerator SaltGenerator { get; set; }
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
        public static IEntityValidator<SharedMeme> SharedMemeValidator { get; set; }
        public static IEntityValidator<Collection> CollectionValidator { get; set; }
        public static IEntityValidator<CollectionItemDetail> CollectionItemDetailValidator { get; set; }
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
        public static IRepository<SharedMeme> SharedMemeRepository { get; set; }
        public static IRepository<Collection> CollectionRepository { get; set; }
        public static IRepository<CollectionItemDetail> CollectionItemDetailRepository { get; set; }
        public static IHttpContextAccessor HttpContextAccessor { get; set; }
        public static IUserService UserService { get; set; }
        public static ICommentService CommentService { get; set; }
        public static IMemeFetcherService MemeFetcherService { get; set; }
        public static IFollowerService FollowerService { get; set; }
        public static IMemeSharingService MemeSharingService { get; set; }
        public static IMemeService MemeService { get; set; }
        public static ICollectionService CollectionService { get; set; }
        public static ICollectionItemDetailService CollectionItemDetailService { get; set; }
        public static IUserManager UserManager { get; set; }

        public ServiceTestsBase()
        {
            Console.WriteLine($"Service: {GetType().Name}");
        }
    }
}
