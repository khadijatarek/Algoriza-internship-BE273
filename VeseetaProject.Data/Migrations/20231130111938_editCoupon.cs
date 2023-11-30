using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeseetaProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class editCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Coupons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Coupons");
        }
    }
}
