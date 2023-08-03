﻿using System;
namespace Async2.Models
{
	public class Hotel
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }


        public Hotel()
		{
		}
	}
}

