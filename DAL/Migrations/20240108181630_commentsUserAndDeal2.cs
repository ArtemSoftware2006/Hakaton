using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class commentsUserAndDeal2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Deals_DealId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_CreatorUserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "CommentDeals");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_DealId",
                table: "CommentDeals",
                newName: "IX_CommentDeals_DealId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CreatorUserId",
                table: "CommentDeals",
                newName: "IX_CommentDeals_CreatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentDeals",
                table: "CommentDeals",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CommentUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentUsers_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentUsers_UserId",
                table: "CommentUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentUsers_UserId1",
                table: "CommentUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDeals_Deals_DealId",
                table: "CommentDeals",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDeals_Users_CreatorUserId",
                table: "CommentDeals",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentDeals_Deals_DealId",
                table: "CommentDeals");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDeals_Users_CreatorUserId",
                table: "CommentDeals");

            migrationBuilder.DropTable(
                name: "CommentUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentDeals",
                table: "CommentDeals");

            migrationBuilder.RenameTable(
                name: "CommentDeals",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDeals_DealId",
                table: "Comments",
                newName: "IX_Comments_DealId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentDeals_CreatorUserId",
                table: "Comments",
                newName: "IX_Comments_CreatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Deals_DealId",
                table: "Comments",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CreatorUserId",
                table: "Comments",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
