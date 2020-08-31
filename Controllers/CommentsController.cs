using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DwitterApp.Entities;
using DwitterApp.Helpers;
using DwitterApp.IServices;
using DwitterApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DwitterApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/comments
        [HttpGet("comments")]
        public async Task<ActionResult<List<CommentModel>>> GetComments()
        {
            return await _commentService.GetCommentsAsync();
        }

        // GET: api/comments/post/2
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<List<CommentModel>>> GetCommentsByPostId(int postId)
        {
            return await _commentService.GetCommentsByPostIdAsync(postId);
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentModel>> GetComment(int id)
        {
            CommentModel comment = await _commentService.GetCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/comments/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentModel>> UpdateComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            try
            {
                return await _commentService.UpdateCommentAsync(id, comment);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult<CommentModel>> PostComment(Comment comment)
        {
            CommentModel commentModel = await _commentService.CreateCommentAsync(comment);
            return commentModel;
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}")]
        public ActionResult<CommentModel> DeleteComment(int id)
        {
            try
            {
                _commentService.DeleteCommentAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
