using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeseetaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedAdminDataandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "787c9f7b-3023-4317-ab17-c56850c13e70", null, "Patient", "PATIENT" },
                    { "9d12de1e-84b5-4cf3-b36c-025e622446c6", null, "Doctor", "DOCTOR" },
                    { "ef41d6a0-8f47-45f2-8421-6492a7590ffe", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "05c4f749-682a-4386-9b37-106c003468de", 0, "71fd5738-dd3a-417b-9e9b-827f293ff041", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@veseeta.com", false, "admin", 1, null, "admin", false, null, null, null, "AQAAAAIAAYagAAAAEEYetuNihsj9I7VUHcenIG9vOsI64eIuEXsfaESK2/Z8WeutRMghQ97SaScXJ/sKaw==", "1234567890", false, "114b7720-9574-48fd-b998-e6181238f090", false, "Admin", "admin@veseeta.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ef41d6a0-8f47-45f2-8421-6492a7590ffe", "05c4f749-682a-4386-9b37-106c003468de" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "787c9f7b-3023-4317-ab17-c56850c13e70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d12de1e-84b5-4cf3-b36c-025e622446c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef41d6a0-8f47-45f2-8421-6492a7590ffe");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "Admin", "05c4f749-682a-4386-9b37-106c003468de" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05c4f749-682a-4386-9b37-106c003468de");
        }
    }
}
