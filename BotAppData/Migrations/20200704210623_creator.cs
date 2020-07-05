using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotAppData.Migrations
{
    public partial class creator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Ages_AgeId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupTypes_GroupTypeId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Products_ProductId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Products_ProductId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Subscriptions",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Groups",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "GroupTypeId",
                table: "Groups",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AgeId",
                table: "Groups",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Funnels",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Ages_AgeId",
                table: "Groups",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "AgeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupTypes_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId",
                principalTable: "GroupTypes",
                principalColumn: "GroupTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Products_ProductId",
                table: "Groups",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Products_ProductId",
                table: "Subscriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Ages_AgeId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupTypes_GroupTypeId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Products_ProductId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Products_ProductId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Funnels");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Subscriptions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Groups",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupTypeId",
                table: "Groups",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgeId",
                table: "Groups",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Ages_AgeId",
                table: "Groups",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "AgeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupTypes_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId",
                principalTable: "GroupTypes",
                principalColumn: "GroupTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Products_ProductId",
                table: "Groups",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Products_ProductId",
                table: "Subscriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
