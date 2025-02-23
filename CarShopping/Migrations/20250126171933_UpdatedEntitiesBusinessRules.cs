using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopping.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesBusinessRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dealership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carQuantity = table.Column<int>(type: "int", nullable: false),
                    employeesQuantity = table.Column<int>(type: "int", nullable: false),
                    monthRevenue = table.Column<double>(type: "float", nullable: false),
                    amountAvaliableCars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealership", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mileage = table.Column<int>(type: "int", nullable: false),
                    fabricationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sellingValue = table.Column<double>(type: "float", nullable: false),
                    isSold = table.Column<bool>(type: "bit", nullable: false),
                    isAvaliable = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_car_dealership_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "dealership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_car_OwnerId",
                table: "car",
                column: "OwnerId");
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
