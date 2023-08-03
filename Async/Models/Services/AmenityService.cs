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

            var amenity = new Amenity
            {
                Name = amenityDTO.Name
            };

            _context.Entry(amenity).State = EntityState.Added;
           
            await _context.SaveChangesAsync();


            AmenityDTO addedAmenity = await GetAmenity(amenity.ID);

            return addedAmenity;
        }




      

        public async Task<List<AmenityDTO>> GetAllAmenities()
        {
            var amenities = await _context.Amenities.Include(a => a.RoomAmenities).ToListAsync();

            var amenityDTO = amenities.Select(a => new AmenityDTO
            {
                ID = a.ID,
                Name = a.Name,

            }).ToList();

            return amenityDTO;

        }





        public async Task<AmenityDTO> GetAmenity(int amenityId)
        {
            Amenity amenity = await _context.Amenities.FindAsync(amenityId);

            if (amenity == null)
            {
                return null;
            }

            var amenityDTO = new AmenityDTO
            {
                ID = amenity.ID,
                Name = amenity.Name,
            };

            return amenityDTO;

           
        

         
        }



        public async Task<Amenity> UpdateAmenity(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return amenity;

        }


        public async Task DeleteAmenity(int amenityId)
        {
            Amenity amenity = await _context.Amenities.FindAsync(amenityId);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }
    }
}

