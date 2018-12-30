using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Abstractions
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<TEntity> _entityValidator;

        protected RepositoryBase(ApplicationDbContext applicationDbContext, IEntityValidator<TEntity> entityValidator)
        {
            _applicationDbContext = applicationDbContext;
            _entityValidator = entityValidator;
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            _entityValidator.Validate(entity);
            _applicationDbContext.Set<TEntity>().Add(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingEntityFailedException($"The entity of type {typeof(TEntity).Name} could not be created.");
            }
            return saveSuccess;
        }
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            if (!await _applicationDbContext.Set<TEntity>().ContainsAsync(entity))
                throw new EntityNotFoundException($"The entity of type {typeof(TEntity).Name} could not be found.");
            _applicationDbContext.Set<TEntity>().Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingEntityFailedException($"The entity of type {typeof(TEntity).Name} could not be deleted.");
            }
            return saveSuccess;
        }
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _applicationDbContext.Set<TEntity>().AnyAsync(predicate);
        }
        public virtual async Task<IEnumerable<TEntity>> FindManyByExpressionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _applicationDbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntity> FindSingleByExpressionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity entity = await _applicationDbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
            if (entity == default)
                throw new EntityNotFoundException($"The entity of type {typeof(TEntity).Name} could not be found by the predicate.");
            return entity;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _applicationDbContext.Update(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new UpdatingEntityFailedException($"The entity of type {typeof(TEntity).Name} failed to update.");
            }
            return saveSuccess;
        }
    }
}
