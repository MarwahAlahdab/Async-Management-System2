using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async2.Data;
using Async2.Models;
using Async2.Models.Interfaces;
using Async2.Models.DTO;
using Async2.Models.Services;
using Microsoft.AspNetCore.Authorization;

namespace Async2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly IRoom _room;

        public RoomController(IRoom room)
        {
            _room = room;
        }


        // GET: api/Room
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<RoomDTO>>> GetAllRooms()
        {
            try
            {
                var rooms = await _room.GetAllRooms();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Room/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int Id)
        {
          
                var room = await _room.GetRoom(Id);

                if (room == null)
                    return NotFound();

                return Ok(room);
          
        }

        // PUT: api/Room/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int roomId, Room room)
        {
            if (roomId != room.ID || !ModelState.IsValid)
                return BadRequest();

            var updatedRoom = await _room.UpdateRoom(room);

            if (updatedRoom == null)
                return NotFound();

            return Ok(updatedRoom);
        }




        // POST: api/Room
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "create")]
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> AddRoom(RoomDTO roomDTO)
        {
            try
            {
                // Call the RoomService to add the new room
                var createdRoomDTO = await _room.AddRoom(roomDTO);

                // Return the newly created RoomDTO along with a status of 201 Created
                return CreatedAtAction(nameof(GetRoom), new { id = createdRoomDTO.ID }, createdRoomDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }



        // DELETE: api/Room/5
        [Authorize(Policy = "delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var room = await _room.GetRoom(roomId);

            if (room == null)
                return NotFound();

            await _room.DeleteRoom(roomId);
            return NoContent();
        }

      
    }
}
