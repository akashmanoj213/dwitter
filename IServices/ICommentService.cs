using System.Collections.Generic;
using System.Threading.Tasks;
using DwitterApp.Entities;
using DwitterApp.Models;

namespace DwitterApp.IServices
{
    public interface ICommentService
    {
        Task<List<CommentModel>> GetCommentsAsync();
        Task<List<CommentModel>> GetCommentsByPostIdAsync(int userId);
        Task<CommentModel> GetCommentAsync(int id);
        Task<CommentModel> UpdateCommentAsync(int id, Comment comment);
        Task<CommentModel> CreateCommentAsync(Comment comment);
        void DeleteCommentAsync(int id);
    }
}