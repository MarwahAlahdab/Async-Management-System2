using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
	public interface IRoom
	{

        //Add a new room to DB
        Task<RoomDTO> AddRoom(RoomDTO roomDTO);

        /// Retrieves a specific room from DB by ID.
        Task<RoomDTO> GetRoom(int roomId);

        /// Retrieves all rooms and return a list of RoomDTO
        Task<List<RoomDTO>> GetAllRooms();

        //Updates an existing room in the database.
        Task<Room> UpdateRoom(Room room);

        // deletes a specific room byID.

        Task DeleteRoom(int roomId);
    }
}

