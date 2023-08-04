using System;
using Async2.Data;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async2.Models.Services
{
	public class HotelRoomRepository : IHotelRoom
    {

        private readonly AsyncDBContext _context;

        public HotelRoomRepository(AsyncDBContext context)
        {
            _context = context;
        }




        public async Task<HotelRoomDTO> AddHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            //hotelRoom object and populate to DTO
            var hotelRoom = new HotelRoom
            {
                RoomId = hotelRoomDTO.RoomId,
                HotelId = hotelRoomDTO.HotelId,
                RoomNumber = hotelRoomDTO.RoomNumber,
                Rate = hotelRoomDTO.Rate,
                PetFriendly = hotelRoomDTO.PetFriendly
            };

            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();

            return hotelRoomDTO;
        }


        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, int hotelId)
        {
            // Get the object from DB by id
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.RoomId == roomId && hr.HotelId == hotelId)
                .FirstOrDefaultAsync();

            if (hotelRoom == null)
                return null;

            //convert to DTO
            var hotelRoomDTO = new HotelRoomDTO
            {
                RoomId = hotelRoom.RoomId,
                HotelId = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly
            };

            return hotelRoomDTO;
        }


        public async Task<List<HotelRoomDTO>> GetAllHotelRooms(int hotelId)
        {
            //get all hotelRooms for a specific hotel by ID
            var hotelRooms = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId)
                .ToListAsync();

            // Create a list of HotelRoomDTO objects to store the details of the retrieved hotel rooms.

            var hotelRoomDTOs = hotelRooms.Select(hotelRoom => new HotelRoomDTO
            {
                RoomId = hotelRoom.RoomId,
                HotelId = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly
            }).ToList();

            return hotelRoomDTOs;
        }

        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {

            _context.Entry(hotelRoom).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        ///// Deletes a specific hotelRoom based room ID and hotel ID.
        public async Task DeleteHotelRoom(int roomId, int hotelId)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.RoomId == roomId && hr.HotelId == hotelId)
                .FirstOrDefaultAsync();

            if (hotelRoom != null)
            {
                _context.HotelRooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();
            }
        }










    }






	}


