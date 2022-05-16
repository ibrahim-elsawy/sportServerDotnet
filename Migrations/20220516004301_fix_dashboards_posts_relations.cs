using Microsoft.EntityFrameworkCore.Migrations;

namespace sportServerDotnet.Migrations
{
    public partial class fix_dashboards_posts_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Dashboards_DashboardId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_DashboardId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DashboardId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "DashboardPost",
                columns: table => new
                {
                    DashboardsId = table.Column<int>(type: "integer", nullable: false),
                    PostsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardPost", x => new { x.DashboardsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_DashboardPost_Dashboards_DashboardsId",
                        column: x => x.DashboardsId,
                        principalTable: "Dashboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DashboardPost_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardPost_PostsId",
                table: "DashboardPost",
                column: "PostsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardPost");

            migrationBuilder.AddColumn<int>(
                name: "DashboardId",
                table: "Posts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_DashboardId",
                table: "Posts",
                column: "DashboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Dashboards_DashboardId",
                table: "Posts",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
