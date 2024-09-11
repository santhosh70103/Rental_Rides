using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rental_Rides.Migrations
{
    /// <inheritdoc />
    public partial class DBUpdate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Rented_Car_Rental_Id",
                table: "Refund");

            migrationBuilder.AlterColumn<int>(
                name: "Rental_Id",
                table: "Refund",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Refund_Status",
                table: "Refund",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Refund_Price",
                table: "Refund",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<int>(
                name: "Customer_ID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_ID",
                table: "Orders",
                column: "Customer_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders",
                column: "Customer_ID",
                principalTable: "Customers",
                principalColumn: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Rented_Car_Rental_Id",
                table: "Refund",
                column: "Rental_Id",
                principalTable: "Rented_Car",
                principalColumn: "Rental_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_ID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Refund_Rented_Car_Rental_Id",
                table: "Refund");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Customer_ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Customer_ID",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Rental_Id",
                table: "Refund",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Refund_Status",
                table: "Refund",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Refund_Price",
                table: "Refund",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Rented_Car_Rental_Id",
                table: "Refund",
                column: "Rental_Id",
                principalTable: "Rented_Car",
                principalColumn: "Rental_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
