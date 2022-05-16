using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sportServerDotnet.Migrations
{
    public partial class fix_user_profile_challenge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Challenge_ChallengeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profile_UserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profile_UserProfileId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChallengeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserProfileId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<List<string>>(
                name: "Followers",
                table: "Profile",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Following",
                table: "Profile",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Users_Id",
                table: "Challenge",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Followers",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Following",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Users_Id",
                table: "Challenge");

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId1",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChallengeId",
                table: "AspNetUsers",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileId",
                table: "AspNetUsers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserProfileId1",
                table: "AspNetUsers",
                column: "UserProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Challenge_ChallengeId",
                table: "AspNetUsers",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profile_UserProfileId",
                table: "AspNetUsers",
                column: "UserProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profile_UserProfileId1",
                table: "AspNetUsers",
                column: "UserProfileId1",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
