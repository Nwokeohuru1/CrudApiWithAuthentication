using CrudApiWithAuthentication.Data;
using CrudApiWithAuthentication.Helpers;
using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApiWithAuthentication.Repository
{
    public class UserServices : IUserServices
    {
        private readonly DbContextConn _dbContextConn;
        public UserServices(DbContextConn dbContextConn)
        {
            _dbContextConn= dbContextConn;
        }
        public async Task<bool> CreateUser(User user)
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
                await _dbContextConn.SaveChangesAsync();
           return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                return false;
            }
            // user.Delflag = true;
            //_dbContextConn.Update(user);
            _dbContextConn.Remove(user);
            await _dbContextConn.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var user = new List<User>();

            user = await _dbContextConn.users.Where(o => o.Delflag == false).ToListAsync();
            return user;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _dbContextConn.users.Where(u => u.Id == id && u.Delflag == false).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> UpdateUser(User user)
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
            await _dbContextConn.SaveChangesAsync();
            return true;
        }
    }
}
