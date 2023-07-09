using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiWithAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IJWTservices _jWTservices;
        public UserController(IUserServices userServices, IJWTservices jWTservices)
        {
            _userServices = userServices;
            _jWTservices = jWTservices;
        }


        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public IActionResult Authentication(UserLogin userLogin)
        {
            var token = _jWTservices.Authenticate(userLogin);
            if (token == null)
            {
                return Unauthorized();
            }
            return StatusCode(StatusCodes.Status200OK, token);
        }


        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userServices.GetAllUsers();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userServices.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("Delete")]
       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userServices.DeleteUser(id);
            return StatusCode(StatusCodes.Status200OK, "User Deleted!!!");
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var Createuser = await _userServices.CreateUser(user);
            return StatusCode(StatusCodes.Status201Created, "User created Successfully!");
        }

        [HttpPost]
        [Route("Update")]
       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUser(User user)
        {  
            await _userServices.UpdateUser(user);
         
            return StatusCode(StatusCodes.Status200OK, "Successfully Updated");
        }






    }
}
