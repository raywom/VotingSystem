using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TenthMigration123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_AspNetUsers_AppUserId",
                table: "Voters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voters",
                table: "Voters");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Voters",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voters",
                table: "Voters",
                columns: new[] { "Id", "AppUserId", "PollId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_AspNetUsers_AppUserId",
                table: "Voters",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_AspNetUsers_AppUserId",
                table: "Voters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voters",
                table: "Voters");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Voters",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voters",
                table: "Voters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_AspNetUsers_AppUserId",
                table: "Voters",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
