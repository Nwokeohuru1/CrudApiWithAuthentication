using CrudApiWithAuthentication.Data;
using CrudApiWithAuthentication.Helpers;
using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
                await _dbContextConn.AddAsync(newUser);
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
            Log.Information("GetAllUsers Called || || ||");
            var user = new List<User>();

            user = await _dbContextConn.users.Where(o => o.Delflag == false).ToListAsync();
           // Log.Information("GetAllUser Result => {@user}", user);
            return user;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _dbContextConn.users.Where(u => u.Id == id && u.Delflag == false).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserId(int id)
        {
            var user = await _dbContextConn.users.FindAsync(id);
            return user;
            
        }

        public async Task<bool> UpdateUser(User user)
        {
            
            var password = PasswordHash.Hash(user.Password);
            
            var userToUpdate = await GetUser(user.Id);
                userToUpdate.Id = user.Id;
                userToUpdate.Password = password;
                userToUpdate.Delflag = user.Delflag;
                userToUpdate.Username = user.Username;
                userToUpdate.Role = user.Role;
                userToUpdate.Address = user.Address;
            
            _dbContextConn.users.Update(userToUpdate);
            await _dbContextConn.SaveChangesAsync();
            return true;
        }

    }
}
