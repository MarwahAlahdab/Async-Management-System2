using System;
using Async2.Data;
using Async2.Models.DTO;
using Async2.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async2.Models.Services
{
	public class AmenityService : IAmenity
	{
        private readonly AsyncDBContext _context;

        public AmenityService(AsyncDBContext context)
        {
            _context = context;
        }
      
        
        public async Task<AmenityDTO> AddAmenity(AmenityDTO amenityDTO)
        {
            // Create a new Amenity entity
            var amenity = new Amenity
            {
                Name = amenityDTO.Name
            };

            _context.Entry(amenity).State = EntityState.Added;
           
            await _context.SaveChangesAsync();

            // Retrieve the added Amenity with its ID
            AmenityDTO addedAmenity = await GetAmenity(amenity.ID);

            return addedAmenity;
        }





        // Return a list of all Amenities from the database.

        public async Task<List<AmenityDTO>> GetAllAmenities()
        {
            // Retrieve all Amenities from DB along with their associated RoomAmenities.

            var amenities = await _context.Amenities.Include(a => a.RoomAmenities).ToListAsync();

            // Convert the list of Amenities to a list of AmenityDTO objects.

            var amenityDTO = amenities.Select(a => new AmenityDTO
            {
                ID = a.ID,
                Name = a.Name,

            }).ToList();

            return amenityDTO;

        }




        //Get amenity By ID
        public async Task<AmenityDTO> GetAmenity(int amenityId)
        {
            // Find the Amenity by ID 

            Amenity amenity = await _context.Amenities.FindAsync(amenityId);

            if (amenity == null)
            {
                return null;
            }

            // Convert the Amenity to an AmenityDTO.

            var amenityDTO = new AmenityDTO
            {
                ID = amenity.ID,
                Name = amenity.Name,
            };

            return amenityDTO;

           
        

         
        }



        public async Task<Amenity> UpdateAmenity(Amenity amenity)
        {
            // Set the state of Amenity object to Modified,so it will be updated in DB.

            _context.Entry(amenity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Return the updated Amenity object.
            return amenity;

        }


        public async Task DeleteAmenity(int amenityId)
        {
            Amenity amenity = await _context.Amenities.FindAsync(amenityId);

            // If its found, set its state to Deleted so it will be deleted in DB.
            _context.Entry(amenity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

        }
    }
}

