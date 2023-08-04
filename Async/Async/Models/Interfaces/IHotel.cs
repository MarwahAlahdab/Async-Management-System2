using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
	public interface IHotel
	{
        //Add a new Hotel to DB
        //The DTO representing the room to be added
        Task<HotelDTO> AddHotel(HotelDTO hotelDTO);

        //Get hotel by Id 
        Task<HotelDTO> GetHotel(int hotelId);

        // Get all hotels and return them as a list of HotelDTO
        Task<List<HotelDTO>> GetAllHotels();

        //update existing hotel
        Task<Hotel> UpdateHotel(Hotel hotel);

        //delete by id
        Task DeleteHotel(int hotelId);

    

    }
}

