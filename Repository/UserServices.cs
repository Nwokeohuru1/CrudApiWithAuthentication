using CrudApiWithAuthentication.Data;
using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;

namespace CrudApiWithAuthentication.Repository
{
    public class UserServices : IUserServices
    {
        private readonly DbContextConn _dbContextConn;
        public UserServices(DbContextConn dbContextConn)
        {
            _dbContextConn= dbContextConn;
        }
        public bool CreateUser(User user)
        {
            var Createuser = _dbContextConn.Add(user);
            _dbContextConn.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user == null)
            {
                return false;
            }
            // user.Delflag = true;
            //_dbContextConn.Update(user);
            _dbContextConn.Remove(user);
            _dbContextConn.SaveChanges();
            return true;
        }

        public List<User> GetAllUsers()
        {
            var user = new List<User>();

            user = _dbContextConn.users.Where(o => o.Delflag == false).ToList();
            return user;
        }

        public User GetUser(int id)
        {
            var user = _dbContextConn.users.Where(u => u.Id == id && u.Delflag == false).FirstOrDefault();
            return user;
        }

        public bool UpdateUser(User user)
        {
            var Updateuser = _dbContextConn.users.Update(user);
            _dbContextConn.SaveChanges();
            return true;
        }
    }
}
