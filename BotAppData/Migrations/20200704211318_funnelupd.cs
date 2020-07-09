using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotAppData.Migrations
{
    public partial class funnelupd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunnelLevels_Funnels_FunnelId",
                table: "FunnelLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Funnels_PageTemplates_PageTemplateId",
                table: "Funnels");

            migrationBuilder.AlterColumn<Guid>(
                name: "PageTemplateId",
                table: "Funnels",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FunnelId",
                table: "FunnelLevels",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FunnelLevels_Funnels_FunnelId",
                table: "FunnelLevels",
                column: "FunnelId",
                principalTable: "Funnels",
                principalColumn: "FunnelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funnels_PageTemplates_PageTemplateId",
                table: "Funnels",
                column: "PageTemplateId",
                principalTable: "PageTemplates",
                principalColumn: "PageTemplateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FunnelLevels_Funnels_FunnelId",
                table: "FunnelLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Funnels_PageTemplates_PageTemplateId",
                table: "Funnels");

            migrationBuilder.AlterColumn<Guid>(
                name: "PageTemplateId",
                table: "Funnels",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FunnelId",
                table: "FunnelLevels",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_FunnelLevels_Funnels_FunnelId",
                table: "FunnelLevels",
                column: "FunnelId",
                principalTable: "Funnels",
                principalColumn: "FunnelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Funnels_PageTemplates_PageTemplateId",
                table: "Funnels",
                column: "PageTemplateId",
                principalTable: "PageTemplates",
                principalColumn: "PageTemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
