using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental_Rides.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rented_Date",
                table: "Rented_Car",
                newName: "PickUp_Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickUp_Date",
                table: "Rented_Car",
                newName: "Rented_Date");
        }
    }
}
