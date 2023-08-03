using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
	public interface IRoom
	{

        Task<RoomDTO> AddRoom(RoomDTO roomDTO);
        Task<RoomDTO> GetRoom(int roomId);
        Task<List<RoomDTO>> GetAllRooms();
        Task<Room> UpdateRoom(Room room);
        Task DeleteRoom(int roomId);
    }
}

