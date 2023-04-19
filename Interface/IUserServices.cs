using CrudApiWithAuthentication.Models;

namespace CrudApiWithAuthentication.Interface
{
    public interface IUserServices
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
