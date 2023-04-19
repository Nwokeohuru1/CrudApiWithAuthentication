using CrudApiWithAuthentication.Data;
using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudApiWithAuthentication.Repository
{
    public class JWTservices : IJWTservices
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextConn _dbContextConn;
        public JWTservices(IConfiguration configuration, DbContextConn dbContextConn)
        {
            _configuration = configuration;
            _dbContextConn = dbContextConn;
        }
        public Tokens Authenticate(UserLogin userLogin)
        {
            string username = userLogin.Email;
            string password = userLogin.Password;
            string role = userLogin.Role;
            var userRecords = _dbContextConn.users.ToList();
            foreach (var item in userRecords)
            {
                if (item.Username == username && item.Password == password && item.Role == role)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var tokenkey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(
                            new Claim[]
                            {
                            new Claim(ClaimTypes.NameIdentifier, password),
                            new Claim(ClaimTypes.Email, username),
                            new Claim(ClaimTypes.Role, role),
                            

                            }),
                        Expires = DateTime.UtcNow.AddMinutes(20),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),
                        SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenhandler.CreateToken(tokenDescriptor);
                    return new Tokens { Token = tokenhandler.WriteToken(token) };
                }
                
            }
            return null;
        }
    }
}
