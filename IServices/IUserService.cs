using System.Collections.Generic;
using System.Threading.Tasks;
using DwitterApp.Entities;

namespace DwitterApp.IServices
{
    public interface IUserService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<User> CreateUserAsync(User user, string password);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        void UpdateUserAsync(User user, string password = null);
        void DeleteUserAsync(int id);
    }
}