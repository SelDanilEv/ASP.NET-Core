using ASP_CORE.Dto;
using ASP_CORE.Model;
using ASP_CORE.Repository;
using ASP_CORE.Services;
using ASP_CORE.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserServices<User> _userService;
        public UserController(DBcontext dBcontext)
        {
            _userService = new UserService(dBcontext);
        }

        /// <summary>
        /// Add new user blalbabla. The id will be set automatically
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] UserDto user)
        {
            var result = _userService.AddUser(new User
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Password = user.Password,
                Status = user.Status,
                Role = user.Role
            });
            if (result == null) return NotFound("User not added");
            return Json(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            var result = _userService.GetAll();
            return Json(new { usersList = result });
        }
        /// <summary>
        /// Set value to xml tytytytytyt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] UserDto user)
        {
            var result = _userService.UpdateUserById(
                new User
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    Password = user.Password,
                    Status = user.Status,
                    Role = user.Role
                });
            if (result == null) return NotFound("User not updated");
            return Json(result);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetUserById(id);
            if (result == null) return NotFound("User not found");
            return Json(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            var result = _userService.DeleteUserById(id);
            if (result == null) return NotFound("User not found");
            return Json(result);
        }
    }
}
