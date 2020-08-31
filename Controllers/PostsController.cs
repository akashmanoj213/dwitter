using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DwitterApp.Entities;
using DwitterApp.Helpers;
using DwitterApp.Models;
using DwitterApp.IServices;
using Microsoft.AspNetCore.Authorization;

namespace DwitterApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<List<PostModel>>> GetPosts()
        {
            return await _postService.GetPostsAsync();
        }

        // GET: api/posts/user/1
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<PostModel>>> GetPostsByUserId(int userId)
        {
            return await _postService.GetPostsByUserIdAsync(userId);
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> GetPost(int id)
        {
            PostModel post = await _postService.GetPostAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PostModel>> UpdatePost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            
            try
            {
                return await _postService.UpdatePostAsync(id, post);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<PostModel>> CreatePost(Post post)
        {
            PostModel postModel = await _postService.CreatePostAsync(post);
            return postModel;
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public ActionResult<Post> DeletePost(int id)
        {
            try
            {
                _postService.DeletePostAsync(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
