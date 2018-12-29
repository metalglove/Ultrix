using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultrix.Application.Converters;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Domain.Entities;

namespace Ultrix.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Meme> _memeRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<Meme> memeRepository)
        {
            _commentRepository = commentRepository;
            _memeRepository = memeRepository;
        }

        public async Task<CommentResultDto> CreateCommentAsync(CommentDto commentDto)
        {
            CommentResultDto commentResultDto = new CommentResultDto
            {
                Message = "Something happened try again later."
            };

            if (!await _memeRepository.ExistsAsync(meme => meme.Id.Equals(commentDto.MemeId)))
                return commentResultDto;

            Comment comment = DtoToEntityConverter.Convert<Comment, CommentDto>(commentDto);
            if (await _commentRepository.CreateAsync(comment))
            {
                commentResultDto.Message = "Successfully added the comment.";
                commentResultDto.Success = true;
            }

            return commentResultDto;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByMemeIdAsync(string memeId)
        {
            IEnumerable<Comment> comments = await _commentRepository.FindManyByExpressionAsync(comment => comment.MemeId.Equals(memeId));
            return comments.Select(EntityToDtoConverter.Convert<CommentDto, Comment>);
        }
    }
}
