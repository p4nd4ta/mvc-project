using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Drinks_Self_Learn.Migrations
{
    public partial class DrinkDetailsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSlideShowUrls",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Drinks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommentViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(maxLength: 200, nullable: false),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true),
                    DrinkId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentViewModel_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "DrinkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentViewModel_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentViewModel_DrinkId",
                table: "CommentViewModel",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentViewModel_IdentityUserId",
                table: "CommentViewModel",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentViewModel");

            migrationBuilder.DropColumn(
                name: "ImageSlideShowUrls",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Drinks");
        }
    }
}
