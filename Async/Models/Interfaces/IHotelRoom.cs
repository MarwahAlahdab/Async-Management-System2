using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
 
        public interface IHotelRoom
        {


            Task<HotelRoomDTO> AddHotelRoom(HotelRoomDTO hotelRoomDTO);

            Task<HotelRoomDTO> GetHotelRoom(int roomId, int hotelId);

            Task<List<HotelRoomDTO>> GetAllHotelRooms(int hotelId);

            Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom);

            Task DeleteHotelRoom(int roomId, int hotelId);
        }
    
}