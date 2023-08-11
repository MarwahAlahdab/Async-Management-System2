using System;
using System.Security.Claims;
using Async2.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async2.Models.Interfaces
{
	public interface IUser
	{
		public Task<UserDto> Register(RegisterUserDto registerUser,ModelStateDictionary modelState);
        public Task<UserDto> Autenticate(string username, string password);

		public Task<UserDto> GetUser(ClaimsPrincipal principal);


        //Task<UserDto> GetDefaultDistrictManagerUserAsync(string email);


    }
}

