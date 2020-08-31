using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DwitterApp.Entities;
using DwitterApp.Helpers;
using DwitterApp.IServices;
using DwitterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DwitterApp.Services
{
    public class PostService : IPostService
    {
        private readonly SqlLiteDataContext _context;
        public PostService(SqlLiteDataContext context)
        {
            _context = context;
        }

        public async Task<PostModel> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            PostModel postModel = await (from p in _context.Posts
                                         join user in _context.Users on p.UserId equals user.Id
                                         where p.Id == post.Id
                                         select new PostModel
                                         {
                                             Id = p.Id,
                                             Content = p.Content,
                                             Username = user.Username,
                                             Likes = p.Likes,
                                             UserId = user.Id
                                         }).FirstOrDefaultAsync();

            return postModel;
        }

        public async void DeletePostAsync(int id)
        {
            Post post = await _context.Posts.FindAsync(id);

            if (post == null)
                throw new AppException("Post not found");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<PostModel> GetPostAsync(int id)
        {
            PostModel postModel = await (from post in _context.Posts
                                         join user in _context.Users on post.UserId equals user.Id
                                         where post.Id == id
                                         select new PostModel
                                         {
                                             Id = post.Id,
                                             Content = post.Content,
                                             Username = user.Username,
                                             Likes = post.Likes,
                                             UserId = user.Id
                                         }).FirstOrDefaultAsync();

            return postModel;
        }

        public async Task<List<PostModel>> GetPostsAsync()
        {
            List<PostModel> posts = await (from post in _context.Posts
                                           join user in _context.Users on post.UserId equals user.Id
                                           orderby post.Id descending
                                           select new PostModel
                                           {
                                               Id = post.Id,
                                               Content = post.Content,
                                               Username = user.Username,
                                               Likes = post.Likes,
                                               UserId = user.Id
                                           }).ToListAsync();

            return posts;
        }

        public async Task<List<PostModel>> GetPostsByUserIdAsync(int userId)
        {
            List<PostModel> posts = await (from post in _context.Posts
                                           join user in _context.Users on post.UserId equals user.Id
                                           where post.UserId == userId
                                           orderby post.Id descending
                                           select new PostModel
                                           {
                                               Id = post.Id,
                                               Content = post.Content,
                                               Username = user.Username,
                                               Likes = post.Likes,
                                               UserId = user.Id
                                           }).ToListAsync();

            return posts;
        }

        public async Task<PostModel> UpdatePostAsync(int id, Post post)
        {
            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"Unable to perform update operation. Error occurerd : {e.Message}");
            }

            User user = await _context.Users.FindAsync(post.UserId);
            
            return new PostModel {
                Content = post.Content,
                Id = post.Id,
                Likes = post.Likes,
                UserId = user.Id,
                Username = user.Username
            };
        }
    }
}