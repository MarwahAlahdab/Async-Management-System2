using Async2.Data;
using Async2.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AsyncTests;

public abstract class Mock : IDisposable
{
    private readonly SqliteConnection _connection;
    protected readonly AsyncDBContext _db;

    public Mock()
    {
        //in memory
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();


        /*
        initializing a new AsyncDBContext instance with SQLite as the database provider,
        using the given connection string
        */
        _db = new AsyncDBContext(new DbContextOptionsBuilder<AsyncDBContext>()
            .UseSqlite(_connection).Options);

        _db.Database.EnsureCreated();


    }



    protected async Task<Room> CreateAndSaveTestRoom()
    {
        var room = new Room() { Name = "Room 1", Layout = RoomLayout.Studio };

        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();
        return room;

    }



    protected async Task<Hotel> CreateAndSaveTestHotel()
    {
        var hotel = new Hotel()
        {
            Name = "Test Hotel",
            StreetAddress = "Test",
            City = "Test",
            State = "Test",
            Country = "Test",
            Phone = "Test"
        };

        _db.Hotels.Add(hotel);
        await _db.SaveChangesAsync();

        //Assert.NotEqual(0, hotel.Id);

        return hotel;


    }



    protected async Task<HotelRoom> CreateAndSaveTestHotelRoom(int hotelId, int roomId)
    {
        var hotelRoom = new HotelRoom()
        {
            //HotelId = hotelId,
            //RoomId = roomId,
            RoomNumber = 100,
            Rate = 50,
            PetFriendly = false
        };

        _db.HotelRooms.Add(hotelRoom);
        await _db.SaveChangesAsync();

        return hotelRoom;
    }




    public void Dispose()
    {
        //if not null
        _db?.Dispose();
        _connection?.Dispose();


    }
}
