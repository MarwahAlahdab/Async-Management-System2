using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
	public interface IAmenity
	{
        //create new amenity
        Task<AmenityDTO> AddAmenity(AmenityDTO amenityDTO);

        /// Retrieves a specific amenity from DB by ID.
        Task<AmenityDTO> GetAmenity(int amenityId);

        //get all amenities and return them as a list
        Task<List<AmenityDTO>> GetAllAmenities();

        
        Task<Amenity> UpdateAmenity(Amenity amenity);

        //delete specific amenity
        Task DeleteAmenity(int amenityId);
    }
}

