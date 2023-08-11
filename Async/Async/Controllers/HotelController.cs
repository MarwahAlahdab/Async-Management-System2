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
using Async2.Models.Services;
using Async2.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Async2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly IHotel _hotel;

        public HotelController(IHotel hotel)
        {
            _hotel = hotel;
        }




        // GET: api/Hotel
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            var hotels = await _hotel.GetAllHotels();

            if (hotels == null || hotels.Count == 0)
            {
                return NotFound();
            }
            return Ok(hotels);
        }



        // GET: api/Hotel/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotel.GetHotel(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);


        }




        // PUT: api/HotelControllers/5
        [Authorize(Policy = "update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int hotelId, Hotel hotel)
        {
            if (hotelId != hotel.Id || !ModelState.IsValid)
                return BadRequest();

            var updatedHotel = await _hotel.UpdateHotel(hotel);

            if (updatedHotel == null)
                return NotFound();

            return Ok(updatedHotel);
        }




        // POST: api/HotelControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "create")]
        [HttpPost]
        public async Task<ActionResult<HotelDTO>> AddHotel(HotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedHotel = await _hotel.AddHotel(hotelDTO);
            return CreatedAtAction(nameof(GetHotel), new { hotelId = addedHotel.Id }, addedHotel);
        }


        // DELETE: api/HotelControllers/5
        [Authorize(Policy = "delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            var hotel = await _hotel.GetHotel(hotelId);

            if (hotel == null)
                return NotFound();

            await _hotel.DeleteHotel(hotelId);
            return NoContent();
        }

       
    }
}
