using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ultrix.Application.Exceptions;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Repositories
{
    public class MemeRepository : IRepository<Meme>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEntityValidator<Meme> _memeValidator;

        public MemeRepository(ApplicationDbContext applicationDbContext, IEntityValidator<Meme> memeValidator)
        {
            _applicationDbContext = applicationDbContext;
            _memeValidator = memeValidator;
        }

        public async Task<bool> CreateAsync(Meme entity)
        {
            _memeValidator.Validate(entity);
            if (await _applicationDbContext.Memes.ContainsAsync(entity))
                throw new MemeAlreadyExistsException();
            await _applicationDbContext.Memes.AddAsync(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new CreatingMemeFailedException();
            }
            return saveSuccess;
        }

        public async Task<bool> DeleteAsync(Meme entity)
        {
            if (!await _applicationDbContext.Memes.ContainsAsync(entity))
                throw new MemeNotFoundException();
            _applicationDbContext.Memes.Remove(entity);
            int saveResult = await _applicationDbContext.SaveChangesAsync();
            bool saveSuccess;
            try
            {
                saveSuccess = Convert.ToBoolean(saveResult);
            }
            catch (Exception)
            {
                throw new DeletingMemeFailedException();
            }
            return saveSuccess;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Meme, bool>> predicate)
        {
            return await _applicationDbContext.Memes.AnyAsync(predicate);
        }

        public Task<IEnumerable<Meme>> FindManyByExpressionAsync(Expression<Func<Meme, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Meme> FindSingleByExpressionAsync(Expression<Func<Meme, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Meme>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Meme entity)
        {
            throw new NotImplementedException();
        }
    }
}
