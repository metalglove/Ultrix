using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Domain.Exceptions;
using Ultrix.Persistance.Abstractions;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class CollectionItemDetailRepository : RepositoryBase<CollectionItemDetail>
    {
        private readonly AppDbContext _applicationDbContext;
        private readonly IEntityValidator<CollectionItemDetail> _collectionItemDetailValidator;

        public CollectionItemDetailRepository(AppDbContext applicationDbContext, IEntityValidator<CollectionItemDetail> collectionItemDetailValidator) : base(applicationDbContext, collectionItemDetailValidator)
        {
            _applicationDbContext = applicationDbContext;
            _collectionItemDetailValidator = collectionItemDetailValidator;
        }

        public override async Task<bool> CreateAsync(CollectionItemDetail entity)
        {
            _collectionItemDetailValidator.Validate(entity);
            if (await _applicationDbContext.CollectionItemDetails.AnyAsync(collectionItemDetail =>
            collectionItemDetail.CollectionId.Equals(entity.CollectionId) &&
            collectionItemDetail.MemeId.Equals(entity.MemeId)))
                throw new EntityAlreadyExistsException($"The entity of type {typeof(CollectionItemDetail).Name} already exists.");
            _applicationDbContext.CollectionItemDetails.Add(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingEntityFailedException($"The entity of type {typeof(CollectionItemDetail).Name} could not be created.");
            }
            return saveSuccess;
        }
        public override async Task<bool> DeleteAsync(CollectionItemDetail entity)
        {
            if (!await _applicationDbContext.CollectionItemDetails.AnyAsync(collectionItemDetail => 
            collectionItemDetail.CollectionId.Equals(entity.CollectionId) &&
            collectionItemDetail.MemeId.Equals(entity.MemeId) &&
            collectionItemDetail.AddedByUserId.Equals(entity.AddedByUserId)))
                throw new EntityNotFoundException($"The entity of type {typeof(CollectionItemDetail).Name} could not be found.");
            _applicationDbContext.CollectionItemDetails.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingEntityFailedException($"The entity of type {typeof(CollectionItemDetail).Name} could not be deleted.");
            }
            return saveSuccess;
        }
    }
}
