using System;
namespace Async2.Models.DTO
{
    public class HotelRoomDTO
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }

        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }



        public HotelDTO Hotel { get; set; }
        public RoomDTO Room { get; set; }
    }
}

