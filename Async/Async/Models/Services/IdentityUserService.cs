using System;
using System.Security.Claims;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async2.Models.Services
{
    public class IdentityUserService : IUser
    {

        private JwtTokenService tokenService;

        private UserManager<ApplicationUser> userManager;

        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService tokenService)
        {
            userManager = manager;
            this.tokenService = tokenService;
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
                await userManager.AddToRolesAsync(user, registerUser.Roles);

                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                    Roles = await userManager.GetRolesAsync(user),
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
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))
                };
            }

            return null;

        }



        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))
            };



        }

       





        //public async Task<UserDto> GetDefaultDistrictManagerUserAsync(string email)
        //{
        //    var user = await userManager.FindByEmailAsync(email);

        //    if (user != null && await userManager.IsInRoleAsync(user, "District Manager"))
        //    {
        //        return new UserDto
        //        {
        //            Id = user.Id,
        //            Username = user.UserName,
        //            Token = await tokenService.GetToken(user, TimeSpan.FromMinutes(5))
        //        };
        //    }

        //    return null;
        //}








    }
}

