using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentrides_hexa.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Admin_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Admin_Email = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Admin_PhoneNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Admin_Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Admin_ID);
                });

            migrationBuilder.CreateTable(
                name: "Car_Details",
                columns: table => new
                {
                    Car_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Car_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Car_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Car_Model_Year = table.Column<int>(type: "int", nullable: false),
                    Rental_Price_PerHour = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Car_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rental_Price_PerDay = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Available_Cars = table.Column<int>(type: "int", nullable: false),
                    Available_Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fuel_Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    No_of_seats = table.Column<int>(type: "int", nullable: true),
                    Transmission_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Penalty_Amt = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Details", x => x.Car_Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Customer_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Customer_Email = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Customer_PhoneNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Customer_Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Customer_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rented_Car",
                columns: table => new
                {
                    Rental_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Car_Id = table.Column<int>(type: "int", nullable: true),
                    Customer_ID = table.Column<int>(type: "int", nullable: true),
                    Rented_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Expected_Return_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Total_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Penalty_PerDay = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Payment_Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Days_of_Rent = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rented_Car", x => x.Rental_Id);
                    table.ForeignKey(
                        name: "FK_Rented_Car_Car_Details_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "Car_Details",
                        principalColumn: "Car_Id");
                    table.ForeignKey(
                        name: "FK_Rented_Car_Customers_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customers",
                        principalColumn: "Customer_Id");
                });
                 
            migrationBuilder.CreateTable(
                name: "User_Feedback",
                columns: table => new
                {
                    Feedback_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: true),
                    Car_Id = table.Column<int>(type: "int", nullable: true),
                    Feedback_Query = table.Column<string>(type: "varchar(max)", nullable: false),
                    Feedback_Point = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Feedback", x => x.Feedback_Id);
                    table.ForeignKey(
                        name: "FK_User_Feedback_Car_Details_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "Car_Details",
                        principalColumn: "Car_Id");
                    table.ForeignKey(
                        name: "FK_User_Feedback_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Customer_Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rental_Id = table.Column<int>(type: "int", nullable: true),
                    Car_Id = table.Column<int>(type: "int", nullable: true),
                    Order_Status = table.Column<int>(type: "int", nullable: true),
                    Total_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Orders_Rented_Car_Rental_Id",
                        column: x => x.Rental_Id,
                        principalTable: "Rented_Car",
                        principalColumn: "Rental_Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Payment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rental_Id = table.Column<int>(type: "int", nullable: true),
                    Payment_Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Total_Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Payment_Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Payment_Id);
                    table.ForeignKey(
                        name: "FK_Payment_Rented_Car_Rental_Id",
                        column: x => x.Rental_Id,
                        principalTable: "Rented_Car",
                        principalColumn: "Rental_Id");
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                columns: table => new
                {
                    Refund_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rental_Id = table.Column<int>(type: "int", nullable: false),
                    Refund_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Refund_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.Refund_Id);
                    table.ForeignKey(
                        name: "FK_Refund_Rented_Car_Rental_Id",
                        column: x => x.Rental_Id,
                        principalTable: "Rented_Car",
                        principalColumn: "Rental_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Returned_Car",
                columns: table => new
                {
                    Return_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rental_Id = table.Column<int>(type: "int", nullable: true),
                    Actual_Return_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Difference_In_Days = table.Column<int>(type: "int", nullable: true),
                    Penalty = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returned_Car", x => x.Return_Id);
                    table.ForeignKey(
                        name: "FK_Returned_Car_Rented_Car_Rental_Id",
                        column: x => x.Rental_Id,
                        principalTable: "Rented_Car",
                        principalColumn: "Rental_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Rental_Id",
                table: "Orders",
                column: "Rental_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Rental_Id",
                table: "Payment",
                column: "Rental_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_Rental_Id",
                table: "Refund",
                column: "Rental_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rented_Car_Car_Id",
                table: "Rented_Car",
                column: "Car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rented_Car_Customer_ID",
                table: "Rented_Car",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Returned_Car_Rental_Id",
                table: "Returned_Car",
                column: "Rental_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Feedback_Car_Id",
                table: "User_Feedback",
                column: "Car_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Feedback_Customer_Id",
                table: "User_Feedback",
                column: "Customer_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "Returned_Car");

            migrationBuilder.DropTable(
                name: "User_Feedback");

            migrationBuilder.DropTable(
                name: "Rented_Car");

            migrationBuilder.DropTable(
                name: "Car_Details");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
