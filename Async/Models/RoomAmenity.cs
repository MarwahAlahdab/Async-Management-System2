using System;
namespace Async2.Models
{
	public class RoomAmenity
	{
        public int roomId;
        public int amenityId;

        public Room Room { get; set; }
        public Amenity Amenity { get; set; }

        public RoomAmenity()
		{
		}
	}
}

