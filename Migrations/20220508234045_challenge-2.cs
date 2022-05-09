using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace sportServerDotnet.Migrations
{
    public partial class challenge2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "Posts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ChallengeId",
                table: "Posts",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChallengeId",
                table: "AspNetUsers",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Challenge_ChallengeId",
                table: "AspNetUsers",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Challenge_ChallengeId",
                table: "Posts",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Challenge_ChallengeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Challenge_ChallengeId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Challenge");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ChallengeId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChallengeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "AspNetUsers");
        }
    }
}
