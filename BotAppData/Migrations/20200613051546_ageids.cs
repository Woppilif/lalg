using Microsoft.EntityFrameworkCore.Migrations;

namespace BotAppData.Migrations
{
    public partial class ageids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Ages_AgeId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AgeId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Ages_AgeId",
                table: "Users",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "AgeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Ages_AgeId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AgeId",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Ages_AgeId",
                table: "Users",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "AgeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
