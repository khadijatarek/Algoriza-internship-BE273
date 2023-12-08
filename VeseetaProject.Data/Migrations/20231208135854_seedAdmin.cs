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
                    { "52c90521-319e-4ba7-8594-e64a08606196", null, "Patient", "PATIENT" },
                    { "bd67b6b9-efdf-4f5b-9e57-c1325775dfcd", null, "Admin", "ADMIN" },
                    { "fc3c5a52-ac46-445a-afd5-013150936502", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageUrl", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "8a502909-1c53-4ee8-ac5c-5231c2fad025", 0, "e4d6bf97-0780-4287-8b5e-adea9b5886b4", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@veseeta.com", false, "admin", "Male", null, "admin", false, null, null, null, "AQAAAAIAAYagAAAAEGBwdpSsNzKCsB/bW+74k0LSjHTXFEWAy96W9e2sphOllckUxurjuAclmbldTCbA3w==", "1234567890", false, "2e051ffd-0a72-4dba-bba2-50cd7f643a26", false, "Admin", "admin@veseeta.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bd67b6b9-efdf-4f5b-9e57-c1325775dfcd", "8a502909-1c53-4ee8-ac5c-5231c2fad025" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52c90521-319e-4ba7-8594-e64a08606196");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd67b6b9-efdf-4f5b-9e57-c1325775dfcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc3c5a52-ac46-445a-afd5-013150936502");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "Admin", "8a502909-1c53-4ee8-ac5c-5231c2fad025" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a502909-1c53-4ee8-ac5c-5231c2fad025");

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
