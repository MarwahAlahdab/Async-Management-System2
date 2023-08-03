using System;
using Async2.Data;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async2.Models.Services
{
	public class RoomService : IRoom
    {
        private readonly AsyncDBContext _context;

        public RoomService(AsyncDBContext context)
        {
            _context = context;
         
        }

        public async Task<RoomDTO> AddRoom(RoomDTO roomDTO)
        {

            // Create a new Room entity 
            var newRoom = new Room
            {
                Name = roomDTO.Name,
                Layout = roomDTO.Layout
            };

            // Add new Room to DB
            _context.Rooms.Add(newRoom);
            await _context.SaveChangesAsync();

            // Add amenities to the room if any
            if (roomDTO.Amenities != null && roomDTO.Amenities.Any())
            {
                foreach (var amenityDTO in roomDTO.Amenities)
                {
                    // Find Amenity by ID
                    var amenity = await _context.Amenities.FindAsync(amenityDTO.ID);
                    if (amenity != null)
                    {
                        // Create a new RoomAmenity and associate it with the Room
                        var roomAmenity = new RoomAmenity
                        {
                            Room = newRoom,
                            Amenity = amenity
                        };
                        _context.RoomAmenities.Add(roomAmenity);
                    }
                }

                await _context.SaveChangesAsync();
            }

           
            var createdRoomDTO = new RoomDTO
            {
                ID = newRoom.ID,
                Name = newRoom.Name,
                Layout = newRoom.Layout,
                Amenities = roomDTO.Amenities // Copy the Amenities from input DTO to output DTO
            };

            return createdRoomDTO;




        }







        public async Task DeleteRoom(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<RoomDTO>> GetAllRooms()
        {
            var rooms = await _context.Rooms
                      .Include(r => r.RoomAmenities)
                          .ThenInclude(ra => ra.Amenity)
                      .ToListAsync();

            var roomDTOs = rooms.Select(room => new RoomDTO
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout,
                Amenities = room.RoomAmenities.Select(ra => new AmenityDTO
                {
                    ID = ra.Amenity.ID,
                    Name = ra.Amenity.Name
                }).ToList()
            }).ToList();

            return roomDTOs;
        }

        public async Task<RoomDTO> GetRoom(int roomId)
        {
            var room = await _context.Rooms
              .Include(r => r.RoomAmenities)
                  .ThenInclude(ra => ra.Amenity)
              .FirstOrDefaultAsync(r => r.ID == roomId);

            if (room == null)
                return null;

            var roomDTO = new RoomDTO
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout,
                Amenities = room.RoomAmenities.Select(ra => new AmenityDTO
                {
                    ID = ra.Amenity.ID,
                    Name = ra.Amenity.Name
                }).ToList()
            };

            return roomDTO;
        }

        public async Task<Room> UpdateRoom(Room room)
        {
         

            _context.Entry(room).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return room;

        }



        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                roomId = roomId,
                amenityId = amenityId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }




        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(r => r.amenityId == amenityId && r.roomId == roomId);

            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }




    }
}

