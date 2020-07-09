using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotAppData.Migrations
{
    public partial class funnelx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunnelLevels_Lessons_LessonId",
                table: "FunnelLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Funnels_Groups_GroupId",
                table: "Funnels");

            migrationBuilder.DropIndex(
                name: "IX_Funnels_GroupId",
                table: "Funnels");

            migrationBuilder.DropIndex(
                name: "IX_FunnelLevels_LessonId",
                table: "FunnelLevels");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Funnels");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "FunnelLevels");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "FunnelLevels",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "FunnelLevels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FunnelLevels_GroupId",
                table: "FunnelLevels",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FunnelLevels_ProductId",
                table: "FunnelLevels",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FunnelLevels_Groups_GroupId",
                table: "FunnelLevels",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FunnelLevels_Products_ProductId",
                table: "FunnelLevels",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunnelLevels_Groups_GroupId",
                table: "FunnelLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_FunnelLevels_Products_ProductId",
                table: "FunnelLevels");

            migrationBuilder.DropIndex(
                name: "IX_FunnelLevels_GroupId",
                table: "FunnelLevels");

            migrationBuilder.DropIndex(
                name: "IX_FunnelLevels_ProductId",
                table: "FunnelLevels");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "FunnelLevels");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "FunnelLevels");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Funnels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "FunnelLevels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Funnels_GroupId",
                table: "Funnels",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FunnelLevels_LessonId",
                table: "FunnelLevels",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_FunnelLevels_Lessons_LessonId",
                table: "FunnelLevels",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funnels_Groups_GroupId",
                table: "Funnels",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
