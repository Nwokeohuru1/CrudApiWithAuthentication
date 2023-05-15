using CrudApiWithAuthentication.Models;

namespace CrudApiWithAuthentication.Interface
{
    public interface IUserServices
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int id);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
