using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YevmiyeTakip.Api.Migrations
{
    /// <inheritdoc />
    public partial class DailyWage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyWage",
                table: "Workers");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyWage",
                table: "DailyRecords",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyWage",
                table: "DailyRecords");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyWage",
                table: "Workers",
                type: "numeric",
                nullable: true);
        }
    }
}
