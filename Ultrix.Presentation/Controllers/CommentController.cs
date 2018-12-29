using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Ultrix.Application.DTOs;
using Ultrix.Application.Interfaces;
using Ultrix.Presentation.ViewModels.Comment;

namespace Ultrix.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Route("CreateComment"), Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentViewModel createCommentViewModel)
        {
            CommentResultDto commentResultDto = new CommentResultDto
            {
                Message = "Something happened try again later.."
            };

            if (!ModelState.IsValid)
                return Json(commentResultDto);

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CommentDto commentDto = createCommentViewModel.GetCommentDto(userId);
            commentResultDto = await _commentService.CreateCommentAsync(commentDto);
            return Json(commentResultDto);
        }
        [Route("GetComments/{memeId}"), HttpGet]
        public async Task<IActionResult> GetCommentsAsync(string memeId)
        {
            if (string.IsNullOrWhiteSpace(memeId))
                return NotFound();

            IEnumerable<CommentDto> commentDtos = await _commentService.GetCommentsByMemeIdAsync(memeId);
            return PartialView("Comments", new CommentsViewModel(commentDtos, memeId));
        }
    }
}
