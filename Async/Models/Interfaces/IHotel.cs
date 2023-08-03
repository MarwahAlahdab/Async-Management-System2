using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
	public interface IHotel
	{
        Task<HotelDTO> AddHotel(HotelDTO hotelDTO);
        Task<HotelDTO> GetHotel(int hotelId);
        Task<List<HotelDTO>> GetAllHotels();
        Task<Hotel> UpdateHotel(Hotel hotel);
        Task DeleteHotel(int hotelId);

    

    }
}

