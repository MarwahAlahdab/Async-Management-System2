using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Async2.Migrations
{
    /// <inheritdoc />
    public partial class HotelRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PetFriendly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => new { x.RoomId, x.HotelId });
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomAmenities",
                columns: table => new
                {
                    roomId = table.Column<int>(type: "int", nullable: false),
                    amenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenities", x => new { x.roomId, x.amenityId });
                    table.ForeignKey(
                        name: "FK_RoomAmenities_Amenities_amenityId",
                        column: x => x.amenityId,
                        principalTable: "Amenities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomAmenities_Rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_HotelId",
                table: "HotelRooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_amenityId",
                table: "RoomAmenities",
                column: "amenityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "RoomAmenities");
        }
    }
}
