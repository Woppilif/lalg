using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BotAppData.Migrations
{
    public partial class funnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageTemplates",
                columns: table => new
                {
                    PageTemplateId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTemplates", x => x.PageTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Funnels",
                columns: table => new
                {
                    FunnelId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PageTemplateId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    BonusAmount = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funnels", x => x.FunnelId);
                    table.ForeignKey(
                        name: "FK_Funnels_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funnels_PageTemplates_PageTemplateId",
                        column: x => x.PageTemplateId,
                        principalTable: "PageTemplates",
                        principalColumn: "PageTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunnelLevels",
                columns: table => new
                {
                    FunnelLevelId = table.Column<Guid>(nullable: false),
                    LessonId = table.Column<Guid>(nullable: false),
                    FunnelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunnelLevels", x => x.FunnelLevelId);
                    table.ForeignKey(
                        name: "FK_FunnelLevels_Funnels_FunnelId",
                        column: x => x.FunnelId,
                        principalTable: "Funnels",
                        principalColumn: "FunnelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FunnelLevels_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunnelLevels_FunnelId",
                table: "FunnelLevels",
                column: "FunnelId");

            migrationBuilder.CreateIndex(
                name: "IX_FunnelLevels_LessonId",
                table: "FunnelLevels",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Funnels_GroupId",
                table: "Funnels",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Funnels_PageTemplateId",
                table: "Funnels",
                column: "PageTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunnelLevels");

            migrationBuilder.DropTable(
                name: "Funnels");

            migrationBuilder.DropTable(
                name: "PageTemplates");
        }
    }
}
