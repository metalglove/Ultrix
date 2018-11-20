using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Entities.Authentication;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<Collection> _collectionValidator;

        public CollectionRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Collection> collectionValidator)
        {
            _applicationDbContext = applicationDbContext;
            _collectionValidator = collectionValidator;
        }

        public async Task<bool> CreateCollectionAsync(Collection collection)
        {
            if (!_collectionValidator.Validate(collection))
                return false;

            if (await CollectionsAnyByNameExist(collection))
                return false;

            _applicationDbContext.Collections.Add(collection);

            await _applicationDbContext.SaveChangesAsync();
            
            return true;
        }
        public async Task<List<Collection>> GetMyCollectionsAsync(string userName)
        {
            ApplicationUser applicationUser = await _applicationDbContext.Users.SingleAsync(user => user.UserName.Equals(userName));

            return applicationUser.Collections;
        }

        public Task<bool> AddToCollectionAsync(Meme meme, string collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AuthorizeSubscriberOnCollectionAsync(string userId, string collectionId)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CollectionsAnyByNameExist(Collection collection)
        {
            return await _applicationDbContext.Collections.AnyAsync(coll => coll.Name.Equals(collection.Name));
        }

        public Task<bool> DeAuthorizeSubscriberOnCollectionAsync(string userId, string collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCollectionAsync(string collectionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesCollectionNameExistAsync(string collectionName)
        {
            return await _applicationDbContext.Collections.AnyAsync(collection => collection.Name.Equals(collectionName));
        }

        public Task<Collection> GetCollectionAsync(string collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCollectionAsync(string memeId, string collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubscribeToCollectionAsync(string userId, string collectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnSubscribeFromCollectionAsync(string userId, string collectionId)
        {
            throw new NotImplementedException();
        }
        
    }
}
