using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFirstWebAPI.Models;
using WebAPILibrary.Models;
using WebAPILibrary.Services;

namespace TheFirstWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(IMapper mapper, IUserService context)
        {
            _mapper = mapper;
            _userService = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var usersDTO = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_userService.GetUsers());

            //User user = new User();
            //var userDTO = _mapper.Map<UserDTO>(user);
            return Content(JsonConvert.SerializeObject(usersDTO));
            //return new JsonResult(usersDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(user);
            }
            await _userService.AddUserAsync(new User
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Note = user.Note
            });
            return Ok();
        }

        //[HttpPut]
        //public IActionResult EditUser(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return new JsonResult(user);
        //    }
        //    _dbContext.Users.Update(user);
        //    return Ok();
        //}
    }
}
