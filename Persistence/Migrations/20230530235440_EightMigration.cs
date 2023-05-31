using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EightMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Polls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHost",
                table: "Polls",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Polls_AppUserId",
                table: "Polls",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_AppUserId",
                table: "Polls",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_AppUserId",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Polls_AppUserId",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "IsHost",
                table: "Polls");
        }
    }
}
