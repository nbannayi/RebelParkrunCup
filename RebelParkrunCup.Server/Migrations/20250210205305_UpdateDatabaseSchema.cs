using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RebelParkrunCup.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaselineTimeMins",
                table: "Runners");

            migrationBuilder.DropColumn(
                name: "BaselineTimeSecs",
                table: "Runners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaselineTimeMins",
                table: "Runners",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaselineTimeSecs",
                table: "Runners",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
