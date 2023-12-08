using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeseetaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "95f2d17e-f1d2-4c7f-bacd-37616d6c714e", null, "Admin", "ADMIN" },
                    { "b6c8e3c9-6474-44b6-8886-612412464f32", null, "Doctor", "DOCTOR" },
                    { "eb2b2475-c067-4646-86a6-131e1fb99c7d", null, "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95f2d17e-f1d2-4c7f-bacd-37616d6c714e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6c8e3c9-6474-44b6-8886-612412464f32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb2b2475-c067-4646-86a6-131e1fb99c7d");
        }
    }
}
