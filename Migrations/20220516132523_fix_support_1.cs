using Microsoft.EntityFrameworkCore.Migrations;

namespace sportServerDotnet.Migrations
{
    public partial class fix_support_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supports_Comments_CommentId1",
                table: "Supports");

            migrationBuilder.DropForeignKey(
                name: "FK_Supports_Posts_PostId1",
                table: "Supports");

            migrationBuilder.DropForeignKey(
                name: "FK_Supports_Replies_RepliesId1",
                table: "Supports");

            migrationBuilder.DropForeignKey(
                name: "FK_Supports_Replies_ReplyId",
                table: "Supports");

            migrationBuilder.DropIndex(
                name: "IX_Supports_CommentId1",
                table: "Supports");

            migrationBuilder.DropIndex(
                name: "IX_Supports_PostId1",
                table: "Supports");

            migrationBuilder.DropIndex(
                name: "IX_Supports_RepliesId1",
                table: "Supports");

            migrationBuilder.DropIndex(
                name: "IX_Supports_ReplyId",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "CommentId2",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "PostId2",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "RepliesId1",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Supports");

            migrationBuilder.AlterColumn<int>(
                name: "RepliesId",
                table: "Supports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Supports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Supports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Supports_RepliesId",
                table: "Supports",
                column: "RepliesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_Replies_RepliesId",
                table: "Supports",
                column: "RepliesId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supports_Replies_RepliesId",
                table: "Supports");

            migrationBuilder.DropIndex(
                name: "IX_Supports_RepliesId",
                table: "Supports");

            migrationBuilder.AlterColumn<int>(
                name: "RepliesId",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentId2",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostId2",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepliesId1",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
                table: "Supports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Supports_CommentId1",
                table: "Supports",
                column: "CommentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Supports_PostId1",
                table: "Supports",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_Supports_RepliesId1",
                table: "Supports",
                column: "RepliesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Supports_ReplyId",
                table: "Supports",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_Comments_CommentId1",
                table: "Supports",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_Posts_PostId1",
                table: "Supports",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_Replies_RepliesId1",
                table: "Supports",
                column: "RepliesId1",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_Replies_ReplyId",
                table: "Supports",
                column: "ReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
