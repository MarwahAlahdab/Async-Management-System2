﻿using System;
namespace Async2.Models.DTO
{
	public class RegisterUserDto
	{

		public string Username { get; set; }
		public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


		public List<string > Roles { get; set; }

    }
}

