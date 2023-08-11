using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Async2.Migrations
{
    /// <inheritdoc />
    public partial class SeedDistrictManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "District Manager", "f1b92445-e54a-43e0-bc12-d354f3df5a86" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1b92445-e54a-43e0-bc12-d354f3df5a86");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d94674e6-1384-4422-bd37-891674065dd7", 0, "e5fd59d5-b110-4ac5-b1e8-631a6417c358", "d_manager1@example.com", false, false, null, null, "MANAGER1", "AQAAAAIAAYagAAAAEOg9hnWxhGkg7o4L+4HEptyz4LcN9Mqf/Kp9rDzw5rUjqjdPv4UI4/zKAUAZyJhdBg==", null, false, "517cefd1-4e71-48eb-87e3-9f7c415b81b2", false, "manager1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "District Manager", "d94674e6-1384-4422-bd37-891674065dd7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "District Manager", "d94674e6-1384-4422-bd37-891674065dd7" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d94674e6-1384-4422-bd37-891674065dd7");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f1b92445-e54a-43e0-bc12-d354f3df5a86", 0, "3030d473-f4c2-4340-b43f-e76a6e289ecc", "d_manager1@example.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEHMTHFtSxGCik2v4R1qmCyg6mOcHcnbk3o7xHSoJMVMlvTO8bNLuOalx1uWV9QpoVw==", null, false, "fa45e413-70c7-4ea9-828a-4102bae7bf2a", false, "manager1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "District Manager", "f1b92445-e54a-43e0-bc12-d354f3df5a86" });
        }
    }
}
