using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeseetaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedSpecializations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "SpecializationId", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "طبيب أسنان", "Dentist" },
                    { 2, "أخصائي قلب", "Cardiologist" },
                    { 3, "أخصائي جلدية", "Dermatologist" },
                    { 4, "طبيب أطفال", "Pediatrician" },
                    { 5, "جراح عظام", "Orthopedic Surgeon" },
                    { 6, "أخصائي أعصاب", "Neurologist" },
                    { 7, "طبيب نسائي", "Gynecologist" },
                    { 8, "طبيب نفسي", "Psychiatrist" },
                    { 9, "طبيب عيون", "Ophthalmologist" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 9);
        }
    }
}
