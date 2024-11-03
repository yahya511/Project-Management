using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class addRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoleSessions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleSessions_UserId",
                table: "UserRoleSessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleSessions_AspNetUsers_UserId",
                table: "UserRoleSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleSessions_AspNetUsers_UserId",
                table: "UserRoleSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleSessions_UserId",
                table: "UserRoleSessions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserRoleSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
