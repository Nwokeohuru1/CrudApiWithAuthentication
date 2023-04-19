using CrudApiWithAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApiWithAuthentication.Data
{
    public class DbContextConn : DbContext
    {
        public DbContextConn(DbContextOptions<DbContextConn> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<UserLogin> userLogins  { get; set; }
    }
}
