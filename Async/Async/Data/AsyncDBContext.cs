using System;
using Async2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Async2.Data
{
    public class AsyncDBContext : IdentityDbContext <ApplicationUser>
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

            base.OnModelCreating(modelBuilder);

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





        

            SeedRole(modelBuilder, "District Manager",
                "hotel.create", "hotel.read", "hotel.update", "hotel.delete",
                "hotelroom.create", "hotelroom.read", "hotelroom.update", "hotelroom.delete",
                "room.create", "room.read", "room.update", "room.delete",
                "amenity.create", "amenity.read", "amenity.update", "amenity.delete",
                "role.create");


            SeedRole(modelBuilder, "Property Manager",
                "hotelroom.create", "hotelroom.read", "hotelroom.update", "hotelroom.delete",
                "amenity.create", "amenity.read", "amenity.update", "amenity.delete",
                "role.create.agent");


            SeedRole(modelBuilder, "Agent",
                "hotelroom.read", "hotelroom.update",
                "amenity.create", "amenity.delete");


            SeedRole(modelBuilder, "Anonymous",
                "hotel.read", "hotelroom.read", "room.read", "amenity.read");





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







            // Seed default District Manager user
            var hasher = new PasswordHasher<ApplicationUser>();
            var districtManagerUser = new ApplicationUser
            {
                UserName = "manager1",
                NormalizedUserName = "MANAGER1", 
                Email = "d_manager1@example.com"
            };

            districtManagerUser.PasswordHash = hasher.HashPassword(districtManagerUser, "DManager#123");
            districtManagerUser.SecurityStamp = Guid.NewGuid().ToString(); 
            modelBuilder.Entity<ApplicationUser>().HasData(districtManagerUser);

            var districtManagerUserRole = new IdentityUserRole<string>
            {
                RoleId = "District Manager", // Role ID of District Manager role
                UserId = districtManagerUser.Id
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(districtManagerUserRole);


      




        }









        int nextId = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list (the params) and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
              new IdentityRoleClaim<string>
              {
                  Id = nextId++,
                  RoleId = role.Id,
                  ClaimType = "permissions", // This matches what we did in Program.cs
                  ClaimValue = permission
              }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }







    }
}

