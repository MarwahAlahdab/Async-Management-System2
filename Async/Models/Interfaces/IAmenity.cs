using System;
using Async2.Models.DTO;

namespace Async2.Models.Interfaces
{
	public interface IAmenity
	{
        Task<AmenityDTO> AddAmenity(AmenityDTO amenityDTO);
        Task<AmenityDTO> GetAmenity(int amenityId);
        Task<List<AmenityDTO>> GetAllAmenities();
        Task<Amenity> UpdateAmenity(Amenity amenity);
        Task DeleteAmenity(int amenityId);
    }
}

