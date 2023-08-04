using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async2.Data;
using Async2.Models;
using Async2.Models.DTO;
using Async2.Models.Interfaces;

namespace Async2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {



        private readonly IAmenity _amenity;

        public AmenityController(IAmenity amenity)
        {
            _amenity = amenity;
        }




        // GET: api/Amenity

        [HttpGet]
        public async Task<ActionResult<List<AmenityDTO>>> GetAllAmenities()
        {
            try
            {
                var amenities = await _amenity.GetAllAmenities();
                return Ok(amenities);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Amenity/5

        [HttpGet("{amenityId}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int amenityId)
        {
            try
            {
                var amenity = await _amenity.GetAmenity(amenityId);

                if (amenity == null)
                    return NotFound();

                return Ok(amenity);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
        //POST
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> AddAmenity(AmenityDTO amenityDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var addedAmenity = await _amenity.AddAmenity(amenityDTO);
                return CreatedAtAction(nameof(GetAmenity), new { amenityId = addedAmenity.ID }, addedAmenity);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Amenity/5

        [HttpPut("{amenityId}")]
        public async Task<ActionResult<Amenity>> UpdateAmenity(int amenityId, Amenity amenity)
        {
          
                if (amenityId != amenity.ID || !ModelState.IsValid)
                    return BadRequest();

                var updatedAmenity = await _amenity.UpdateAmenity(amenity);

                if (updatedAmenity == null)
                    return NotFound();

                return Ok(updatedAmenity);
          
        }


        // DELETE: api/Amenity/5

        [HttpDelete("{amenityId}")]
        public async Task<ActionResult> DeleteAmenity(int amenityId)
        {
            try
            {
                var amenity = await _amenity.GetAmenity(amenityId);

                if (amenity == null)
                    return NotFound();

                await _amenity.DeleteAmenity(amenityId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}



//