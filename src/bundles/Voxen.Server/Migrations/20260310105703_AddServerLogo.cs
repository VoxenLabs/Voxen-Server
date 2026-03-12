using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voxen.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddServerLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Server",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoContentType",
                table: "Server",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "LogoContentType",
                table: "Server");
        }
    }
}
