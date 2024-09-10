using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental_Rides.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total_Price",
                table: "Orders",
                type: "decimal(10,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Price",
                table: "Orders");
        }
    }
}
