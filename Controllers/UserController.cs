using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiWithAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public IActionResult GetAllUsers()
        {
            var user = _userServices.GetAllUsers();
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("User")]
        public IActionResult GetUser(int id)
        {
            var user = _userServices.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles ="Admin")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userServices.DeleteUser(id);
            return StatusCode(StatusCodes.Status200OK, "User Deleted!!!");
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateUser(User user)
        {
            var Createuser = _userServices.CreateUser(user);
            return StatusCode(StatusCodes.Status201Created, "User created Successfully!");
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles ="Admin")]
        public IActionResult UpdateUser(User user)
        {
            var updateUser = _userServices.UpdateUser(user);
            return StatusCode(StatusCodes.Status200OK, "Successfully Updated");
        }


















    }
}
