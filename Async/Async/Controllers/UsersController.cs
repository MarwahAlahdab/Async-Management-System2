using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Async2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUser userService;

        public UsersController(IUser service)
        {
            userService = service;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto data)
        {
            var user = await userService.Register(data, this.ModelState);
            if (ModelState.IsValid)
            {
                return user;
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
        {
            var user = await userService.Autenticate(loginDTO.Username, loginDTO.Password);

            if (user == null)
                return Unauthorized();

            return user;

        }


        [AllowAnonymous]
        [HttpGet("Profile")]
        public async Task <ActionResult<UserDto>> Profile()
        {

            return await userService.GetUser(this.User);
        }














        //[HttpGet("district-manager")]
        //[Authorize(Roles = "District Manager")] // Authorize the route for District Managers only
        //public async Task<IActionResult> GetDefaultDistrictManagerUser(string email)
        //{
            
        //        var userDto = await userService.GetDefaultDistrictManagerUserAsync(email);

        //        if (userDto != null)
        //        {
        //            return Ok(userDto);
        //        }
        //        else
        //        {
        //            return NotFound("Default District Manager user not found.");
        //        }
           
        //}






    }
}
