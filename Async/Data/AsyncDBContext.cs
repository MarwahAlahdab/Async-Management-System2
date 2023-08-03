using System;
using Async2.Models;
using Microsoft.EntityFrameworkCore;

namespace Async2.Data
{
    public class AsyncDBContext : DbContext
    {



        public AsyncDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Room>().HasData(
                new Room { ID = 1, Name = "Room 1", Layout = RoomLayout.Studio },
                new Room { ID = 2, Name = "Room 2", Layout = RoomLayout.OneBedroom },
                new Room { ID = 3, Name = "Room 3", Layout = RoomLayout.TwoBedroom }
                );




            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,Name = "Hotel 1",StreetAddress = "Address1",City = "amman",State = "state1",Country = "country 1",Phone = "123-456-7890"
                },
                  new Hotel
                  {
                      Id = 2,
                      Name = "Hotel 2",
                      StreetAddress = "Address2",
                      City = "amman",
                      State = "state2",
                      Country = "country 2",
                      Phone = "123-456-7890"
                  }
              
            );

           

            modelBuilder.Entity<Amenity>().HasData(
            new Amenity { ID = 1, Name = "coffee maker" },
            new Amenity { ID = 2, Name = "air conditioning" },
            new Amenity { ID = 3, Name = "ocean view" }
        );




            modelBuilder.Entity<HotelRoom>().HasKey(hr => new { hr.RoomId, hr.HotelId });
            modelBuilder.Entity<RoomAmenity>().HasKey(
      RoomAmenity => new {
          RoomAmenity.roomId,
          RoomAmenity.amenityId
      }
      );



            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom { RoomId = 1, HotelId = 1, RoomNumber = 101, Rate = 100, PetFriendly = true },
                new HotelRoom { RoomId = 2, HotelId = 1, RoomNumber = 102, Rate = 120, PetFriendly = false },
                new HotelRoom { RoomId = 3, HotelId = 2, RoomNumber = 201, Rate = 150, PetFriendly = true }
            );

            modelBuilder.Entity<RoomAmenity>().HasData(
                new RoomAmenity { roomId = 1, amenityId = 1 },
                new RoomAmenity { roomId = 1, amenityId = 2 },
                new RoomAmenity { roomId = 2, amenityId = 1 },
                new RoomAmenity { roomId = 3, amenityId = 2 }
            );


        }


    }
}

