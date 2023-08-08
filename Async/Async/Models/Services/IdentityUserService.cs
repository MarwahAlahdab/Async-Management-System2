using System;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async2.Models.Services
{
    public class IdentityUserService : IUser
    {

        private UserManager<ApplicationUser> userManager;

        public IdentityUserService(UserManager<ApplicationUser> manager)
        {
            userManager = manager;
        }

       

        public async Task<UserDto> Register(RegisterUserDto registerUser, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            foreach(var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(registerUser.Password) :
                               error.Code.Contains("Email") ? nameof(registerUser.Email) :
                               error.Code.Contains("Username") ? nameof(registerUser.Username) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }



        public async Task<UserDto> Autenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool validPass = await userManager.CheckPasswordAsync(user, password);

            if (validPass)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            return null;

        }










    }
}

