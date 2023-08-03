using System;
namespace Async2.Models
{
	public class HotelRoom
	{
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }

        public HotelRoom()
		{
		}
	}
}

