using System.Collections.Generic;
using System.Threading.Tasks;
using DwitterApp.Entities;
using DwitterApp.Helpers;
using DwitterApp.IServices;
using DwitterApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace DwitterApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly SqlLiteDataContext _context;

        public CommentService(SqlLiteDataContext context)
        {
            _context = context;
        }

        public async Task<CommentModel> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            CommentModel commentModel = await (from c in _context.Comments
                                         join user in _context.Users on c.UserId equals user.Id
                                         where c.Id == comment.Id
                                         select new CommentModel
                                         {
                                             Id = c.Id,
                                             Content = c.Content,
                                             Username = user.Username
                                         }).FirstOrDefaultAsync();

            return commentModel;
        }

        public async void DeleteCommentAsync(int id)
        {
            Comment comment = await _context.Comments.FindAsync(id);

            if (comment == null)
                throw new AppException("Comment not found");

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentModel> GetCommentAsync(int id)
        {
            CommentModel commentModel = await (from comment in _context.Comments
                                         join user in _context.Users on comment.UserId equals user.Id
                                         where comment.Id == id
                                         select new CommentModel
                                         {
                                             Id = comment.Id,
                                             Content = comment.Content,
                                             Username = user.Username
                                         }).FirstOrDefaultAsync();

            return commentModel;
        }

        public async Task<List<CommentModel>> GetCommentsAsync()
        {
            List<CommentModel> comments = await (from comment in _context.Comments
                                           join user in _context.Users on comment.UserId equals user.Id
                                           orderby comment.Id descending
                                           select new CommentModel
                                           {
                                               Id = comment.Id,
                                               Content = comment.Content,
                                               Username = user.Username
                                           }).ToListAsync();

            return comments;
        }

        public async Task<List<CommentModel>> GetCommentsByPostIdAsync(int postId)
        {
            List<CommentModel> comments = await (from comment in _context.Comments
                                           join user in _context.Users on comment.UserId equals user.Id
                                           orderby comment.Id descending
                                           where comment.PostId == postId
                                           select new CommentModel
                                           {
                                               Id = comment.Id,
                                               Content = comment.Content,
                                               Username = user.Username
                                           }).ToListAsync();

            return comments;
        }

        public async Task<CommentModel> UpdateCommentAsync(int id, Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"Unable to perform update operation. Error occurerd : {e.Message}");
            }

            User user = await _context.Users.FindAsync(comment.UserId);
            
            return new CommentModel {
                Content = comment.Content,
                Id = comment.Id,
                Username = user.Username,
            };
        }
    }
}