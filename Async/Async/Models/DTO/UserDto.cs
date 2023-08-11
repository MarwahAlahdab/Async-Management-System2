using System;
namespace Async2.Models.DTO
{
	public class UserDto
	{
        public string Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }


        public IList<string> Roles { get; set; }


    }
}

