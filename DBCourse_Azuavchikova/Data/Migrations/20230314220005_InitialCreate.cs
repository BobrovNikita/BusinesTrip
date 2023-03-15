using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBCourse_Azuavchikova.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateEnter = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesTravelExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesTravelExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinesTrips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Goal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Basis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinesTrips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TravelExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurposePayments = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatePayments = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SumPayments = table.Column<int>(type: "int", nullable: false),
                    NameExpense = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinesTripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypesTravelExpensesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelExpenses_BusinesTrips_BusinesTripId",
                        column: x => x.BusinesTripId,
                        principalTable: "BusinesTrips",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TravelExpenses_TypesTravelExpenses_TypesTravelExpensesId",
                        column: x => x.TypesTravelExpensesId,
                        principalTable: "TypesTravelExpenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinesTrips_EmployeeId",
                table: "BusinesTrips",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelExpenses_BusinesTripId",
                table: "TravelExpenses",
                column: "BusinesTripId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelExpenses_TypesTravelExpensesId",
                table: "TravelExpenses",
                column: "TypesTravelExpensesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelExpenses");

            migrationBuilder.DropTable(
                name: "BusinesTrips");

            migrationBuilder.DropTable(
                name: "TypesTravelExpenses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
