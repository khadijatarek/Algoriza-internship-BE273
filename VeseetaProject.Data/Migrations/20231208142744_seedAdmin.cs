using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeseetaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34c8a3b0-8e00-47f0-aeb3-35fcf7cb9784", null, "Doctor", "DOCTOR" },
                    { "990a8280-14c6-4bf2-8d9b-7cb67cdfb6ec", null, "Admin", "ADMIN" },
                    { "b1faf922-b6b6-43cd-94f8-22c0bb94ba77", null, "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "61d21c34-4841-4238-be05-0ef18f415ac5", 0, "07b18aa0-5e1d-4fdc-9678-db9cb498429a", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@veseeta.com", false, "admin", "Male", null, "admin", false, null, "ADMIN@VESEETA.COM", "ADMIN@VESEETA.COM", "AQAAAAIAAYagAAAAELaoCAUxU1YpkW52HFaueADXydTeNpU5Cv1mtr9B7ZHdOMR5ljN+RAbtHrx/lI33Bw==", "1234567890", false, "afc58824-36c9-4cb8-a456-7241612a3124", false, "Admin", "admin@veseeta.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "990a8280-14c6-4bf2-8d9b-7cb67cdfb6ec", "61d21c34-4841-4238-be05-0ef18f415ac5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34c8a3b0-8e00-47f0-aeb3-35fcf7cb9784");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "990a8280-14c6-4bf2-8d9b-7cb67cdfb6ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1faf922-b6b6-43cd-94f8-22c0bb94ba77");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "Admin", "61d21c34-4841-4238-be05-0ef18f415ac5" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61d21c34-4841-4238-be05-0ef18f415ac5");

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
    }
}
