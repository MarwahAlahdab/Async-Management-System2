using System;
namespace Async2.Models
{
	public class Room
	{

        public int ID { get; set; }
        public string Name { get; set; }
        public RoomLayout Layout { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }
        public List<RoomAmenity> RoomAmenities { get; set; }


        public Room()
		{
		}



	}


	public enum RoomLayout
	{
		Studio = 0,
		OneBedroom = 1,
		TwoBedroom = 2
	}
}

