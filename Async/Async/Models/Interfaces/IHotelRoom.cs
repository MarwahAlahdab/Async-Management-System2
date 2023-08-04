using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
 
        public interface IHotelRoom
        {

        //Add a new HotelRoom to DB

        Task<HotelRoomDTO> AddHotelRoom(HotelRoomDTO hotelRoomDTO);

        //Get HotelRoom based on Id's
        Task<HotelRoomDTO> GetHotelRoom(int roomId, int hotelId);

        //Get all and return as a list of HotelRoomDTO
        Task<List<HotelRoomDTO>> GetAllHotelRooms(int hotelId);

        Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom);

        //delete specific HotelRoom
        Task DeleteHotelRoom(int roomId, int hotelId);
        }
    
}