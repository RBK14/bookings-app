using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Offers",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Offers",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CancelledBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);
        }
    }
}
