using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CruiseShip.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBillProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDetails",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BookingId",
                table: "Bills",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Bookings_BookingId",
                table: "Bills",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Bookings_BookingId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BookingId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "BookingDetails",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
