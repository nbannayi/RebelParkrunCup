using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RebelParkrunCup.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddParkrunIdToRunner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkrunId",
                table: "Runners",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkrunId",
                table: "Runners");
        }
    }
}
