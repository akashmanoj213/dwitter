using System.Collections.Generic;
using System.Threading.Tasks;
using DwitterApp.Entities;
using DwitterApp.Models;

namespace DwitterApp.IServices
{
    public interface IPostService
    {
        Task<List<PostModel>> GetPostsAsync();
        Task<List<PostModel>> GetPostsByUserIdAsync(int userId);
        Task<PostModel> GetPostAsync(int id);
        Task<PostModel> UpdatePostAsync(int id, Post post);
        Task<PostModel> CreatePostAsync(Post post);
        void DeletePostAsync(int id);
    }
}