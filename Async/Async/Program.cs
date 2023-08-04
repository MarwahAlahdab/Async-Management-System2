using Async2.Data;
using Async2.Models.Interfaces;
using Async2.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AsyncDBContext>(options => options.UseSqlServer(connString));

builder.Services.AddControllers();



builder.Services.AddTransient<IHotel, HotelService>();
builder.Services.AddTransient<IRoom, RoomService>();
builder.Services.AddTransient<IAmenity, AmenityService>();
builder.Services.AddTransient<IHotelRoom, HotelRoomRepository>();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "HotelSystemm2",
        Version = "v1",
    });
});





var app = builder.Build();

app.UseSwagger(aptions =>
{
    aptions.RouteTemplate = "/api/{documentName}/swagger.json";
});

app.UseSwaggerUI(aptions =>
{
    aptions.SwaggerEndpoint("/api/v1/swagger.json", "HotelSystemm2");
    aptions.RoutePrefix = string.Empty;
});
//aptions.RoutePrefix = "docs";




app.MapControllers();


//app.MapGet("/", () => "Hello World!");

app.Run();

