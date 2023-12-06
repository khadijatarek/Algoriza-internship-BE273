using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeseetaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "fbed4786-3eed-48b0-8cc4-0e3db7375b0a", 0, "84ca1b86-ecac-4ea5-979e-a87eb776ce35", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@veseeta.com", false, "admin", 1, null, "admin", false, null, null, null, "AQAAAAIAAYagAAAAEKPOWTT0JvhEKYj+WA+TuI7HBoKIU4EmsMoeIkWgMCWkWgDsuV734kYaWEjE4skCcQ==", "1234567890", false, "3ed1eb4f-4c63-43c0-9be2-d423dd2b468a", false, "Admin", "admin@veseeta.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fbed4786-3eed-48b0-8cc4-0e3db7375b0a");
        }
    }
}
