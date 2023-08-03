using System;
using Async2.Data;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async2.Models.Services
{
	public class HotelService : IHotel
	{
        private readonly AsyncDBContext _context;

        public HotelService(AsyncDBContext context)
        {
            _context = context;
        }


        public async Task<HotelDTO> AddHotel(HotelDTO hotelDTO)
        {
            var hotel = new Hotel
            {
                Name = hotelDTO.Name,
                StreetAddress = hotelDTO.StreetAddress,
                City = hotelDTO.City,
                State = hotelDTO.State,
                Country = hotelDTO.Country,
                Phone = hotelDTO.Phone
            };

            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();


            HotelDTO addedHotel = await GetHotel(hotel.Id);

            return addedHotel;

        }





        public async Task<List<HotelDTO>> GetAllHotels()
        {

            var hotels = await _context.Hotels
         .Include(h => h.HotelRooms)
         .ThenInclude(hr => hr.Room)
         .ToListAsync();

            List<HotelDTO> hotelDTOs = new List<HotelDTO>();

            foreach (var hotel in hotels)
            {
                HotelDTO hotelDTO = new HotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Country = hotel.Country,
                    Phone = hotel.Phone,
                };

                if (hotel.HotelRooms != null)
                {
                    hotelDTO.Rooms = hotel.HotelRooms.Select(hr => new HotelRoomDTO
                    {
                        HotelId = hr.HotelId,
                        RoomId = hr.RoomId,
                        RoomNumber = hr.RoomNumber,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFriendly,
                        Room = hr.Room != null ? new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = hr.Room.Layout,
                            Amenities = hr.Room.RoomAmenities?.Select(ra => new AmenityDTO
                            {
                                ID = ra.Amenity.ID,
                                Name = ra.Amenity.Name
                            }).ToList()
                        } : null
                    }).ToList();
                }
                else
                {
                    hotelDTO.Rooms = new List<HotelRoomDTO>();
                }

                hotelDTOs.Add(hotelDTO);
            }

            return hotelDTOs;
        }

     


        public async Task<HotelDTO> GetHotel(int hotelId)
        {
          

        
            var hotel = await _context.Hotels
        .Include(h => h.HotelRooms)
            .ThenInclude(hr => hr.Room)
                .ThenInclude(r => r.RoomAmenities)
                    .ThenInclude(ra => ra.Amenity)
        .FirstOrDefaultAsync(h => h.Id == hotelId);

            if (hotel == null)
                return null;

            var hotelDTO = new HotelDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Country = hotel.Country,
                Phone = hotel.Phone,
                Rooms = hotel.HotelRooms.Select(hr => new HotelRoomDTO
                {
                    HotelId = hr.HotelId,
                    RoomId = hr.RoomId,
                    RoomNumber = hr.RoomNumber,
                    Rate = hr.Rate,
                    PetFriendly = hr.PetFriendly,
                    Room = new RoomDTO
                    {
                        ID = hr.Room.ID,
                        Name = hr.Room.Name,
                        Layout = hr.Room.Layout,
                        Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            ID = ra.Amenity.ID,
                            Name = ra.Amenity.Name
                        }).ToList()
                    }
                }).ToList()
            };

            return hotelDTO;
        }

  

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotel;
        }





        public async Task DeleteHotel(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);

            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }
    }
}

