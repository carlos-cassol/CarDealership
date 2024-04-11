using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopping.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mileage = table.Column<int>(type: "int", nullable: false),
                    fabricationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sellingValue = table.Column<double>(type: "float", nullable: false),
                    isSold = table.Column<bool>(type: "bit", nullable: false),
                    isAvaliable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dealership",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carQuantity = table.Column<int>(type: "int", nullable: false),
                    employeesQuantity = table.Column<int>(type: "int", nullable: false),
                    monthRevenue = table.Column<double>(type: "float", nullable: false),
                    amountAvaliableCars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealership", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "dealership");
        }
    }
}
