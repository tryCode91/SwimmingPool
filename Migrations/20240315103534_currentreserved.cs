using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimmingPool_V1.Migrations
{
    /// <inheritdoc />
    public partial class currentreserved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentReserved",
                table: "Lane",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentReserved",
                table: "Lane");
        }
    }
}
