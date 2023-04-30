using CrudApiWithAuthentication.Data;
using CrudApiWithAuthentication.Helpers;
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
            var pass = PasswordHash.Hash(user.Password);
            var newUser = new User
            { Address = user.Address,
                Username = user.Username,
                Password = pass,
                DateCreated = user.DateCreated,
                Role = user.Role,
                Delflag = user.Delflag,
                Id = user.Id
                
              
            };
            //var Createuser = _dbContextConn.Add(newUser);
             _dbContextConn.Add(newUser);
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
            var password = PasswordHash.Hash(user.Password);
            var UpdatedUser = new User
            {
                Address = user.Address,
                Username = user.Username,
                Password = password,
                DateCreated = user.DateCreated,
                Role = user.Role,
                Delflag = user.Delflag,
                Id = user.Id


            };
            _dbContextConn.users.Update(UpdatedUser);
            _dbContextConn.SaveChanges();
            return true;
        }
    }
}
