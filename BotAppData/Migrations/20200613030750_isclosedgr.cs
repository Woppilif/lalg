using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotAppData.Migrations
{
    public partial class isclosedgr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentsCount",
                table: "Groups");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Groups",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ProductId",
                table: "Groups",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Products_ProductId",
                table: "Groups",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Products_ProductId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ProductId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "StudentsCount",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
