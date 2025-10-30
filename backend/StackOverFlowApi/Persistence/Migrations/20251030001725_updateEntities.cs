using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_UserId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Questions",
                newName: "UserNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                newName: "IX_Questions_UserNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_UserNumber",
                table: "Questions",
                column: "UserNumber",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_UserNumber",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "UserNumber",
                table: "Questions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_UserNumber",
                table: "Questions",
                newName: "IX_Questions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_UserId",
                table: "Questions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
