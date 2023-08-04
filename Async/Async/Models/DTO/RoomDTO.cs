using System;
namespace Async2.Models.DTO
{
	public class RoomDTO
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public RoomLayout Layout { get; set; }

        public List<AmenityDTO> Amenities { get; set; }

    }
}

