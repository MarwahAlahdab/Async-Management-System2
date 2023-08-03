using System;
namespace Async2.Models
{
	public class Amenity
	{
        public int ID { get; set; }
        public string Name { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }



        public Amenity()
		{
		}
	}
}

