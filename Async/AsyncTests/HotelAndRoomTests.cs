using Async2.Data;
using Async2.Models;
using Async2.Models.DTO;
using Async2.Models.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AsyncTests;

public class HotelAndRoomTests : Mock
{

    //RoomService Tests

    [Fact]
    public async Task AddRoom_ReturnRoomDTO_Successfully()
    {
        // Arrange
        var room = await CreateAndSaveTestRoom();

        var roomService = new RoomService(_db);

        // Act
        var roomDTO = new RoomDTO
        {
            ID = room.ID,
            Name = room.Name,
            Layout = room.Layout
        };

        await roomService.AddRoom(roomDTO);

        // Assert
        Assert.NotNull(roomDTO);
        Assert.NotEqual(0, roomDTO.ID);
        Assert.Equal("Room 1", roomDTO.Name);
    }


    [Fact]
    public async Task GetRoom_ReturnRoomDTO_IfRoomExists()
    {
        // Arrange
        var roomService = new RoomService(_db);
        var room = await CreateAndSaveTestRoom();

        // Act
        var roomDTO = await roomService.GetRoom(room.ID);


        Assert.NotNull(roomDTO);
        Assert.Equal(room.ID, roomDTO.ID);
        Assert.Equal(room.Name, roomDTO.Name);
    }




    [Fact]
    public async Task UpdateRoom_Successfully()
    {
        // Arrange
        var roomService = new RoomService(_db);
        var room = await CreateAndSaveTestRoom();
        var updatedName = "Updated";

        // Act
        room.Name = updatedName;
        var updatedRoom = await roomService.UpdateRoom(room);


        Assert.Equal(room.ID, updatedRoom.ID);
        Assert.Equal(updatedName, updatedRoom.Name);

    }


    [Fact]
    public async Task DeleteRoom_Successfully()
    {
        // Arrange
        var roomService = new RoomService(_db);
        var room = await CreateAndSaveTestRoom();

        // Act
        await roomService.DeleteRoom(room.ID);

        var deletedRoom = await roomService.GetRoom(room.ID);

        Assert.Null(deletedRoom);
    }









    //HotelService Tests







    [Fact]
    public async Task AddHotel_ReturnHotelDTO_Successfully()
    {
        var hotelService = new HotelService(_db);

        var hotel = await CreateAndSaveTestHotel();

        var hotelDTO = new HotelDTO
        {
            Id = hotel.Id,
            Name = hotel.Name,
            StreetAddress = hotel.StreetAddress,
            City = hotel.City,
            State = hotel.State,
            Country = hotel.Country,
            Phone = hotel.Phone
        };

        await hotelService.AddHotel(hotelDTO);

        Assert.NotNull(hotelDTO);
        Assert.NotEqual(0, hotelDTO.Id);
        Assert.Equal("Test Hotel", hotelDTO.Name);
    }



    [Fact]
    public async Task GetHotel_ReturnHotelDTO_IfHotelExists()
    {
        var hotelService = new HotelService(_db);
        var hotel = await CreateAndSaveTestHotel();

        var hotelDTO = await hotelService.GetHotel(hotel.Id);

        Assert.Equal(hotel.Id, hotelDTO.Id);
        Assert.Equal(hotel.Name, hotelDTO.Name);
    }



    [Fact]
    public async Task UpdateHotel_Successfully()
    {
        
        var hotelService = new HotelService(_db);
        var hotel = await CreateAndSaveTestHotel();
        var updatedName = "Updated";

        hotel.Name = updatedName;
        var updatedHotel = await hotelService.UpdateHotel(hotel);

        Assert.NotNull(updatedHotel);
        Assert.Equal(hotel.Id, updatedHotel.Id);
        Assert.Equal(updatedName, updatedHotel.Name);
    }



    [Fact]
    public async Task DeleteHotel_Successfully()
    {
        var hotelService = new HotelService(_db);
        var hotel = await CreateAndSaveTestHotel();

        await hotelService.DeleteHotel(hotel.Id);

        var deletedHotel = await hotelService.GetHotel(hotel.Id);
        Assert.Null(deletedHotel);
    }









}
