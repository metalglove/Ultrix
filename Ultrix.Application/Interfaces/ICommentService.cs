using System.Collections.Generic;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;

namespace Ultrix.Application.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceResponseDto> CreateCommentAsync(CommentDto commentDto);
        Task<IEnumerable<CommentDto>> GetCommentsByMemeIdAsync(string memeId);
    }
}
