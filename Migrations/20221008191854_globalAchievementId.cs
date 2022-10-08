using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoretechBack.Migrations
{
    public partial class globalAchievementId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GlobalAchievementId1",
                table: "UserAchievement",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_GlobalAchievementId1",
                table: "UserAchievement",
                column: "GlobalAchievementId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Achievements_GlobalAchievementId1",
                table: "UserAchievement",
                column: "GlobalAchievementId1",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Achievements_GlobalAchievementId1",
                table: "UserAchievement");

            migrationBuilder.DropIndex(
                name: "IX_UserAchievement_GlobalAchievementId1",
                table: "UserAchievement");

            migrationBuilder.DropColumn(
                name: "GlobalAchievementId1",
                table: "UserAchievement");
        }
    }
}
