using Microsoft.EntityFrameworkCore.Migrations;

namespace Drinks_Self_Learn.Migrations
{
    public partial class DrinkDetailsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Drinks_DrinkId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "ImageSlideShowUrls",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommentText",
                table: "Comments",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Drinks_DrinkId",
                table: "Comments",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Drinks_DrinkId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ImageSlideShowUrls",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CommentText",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Drinks_DrinkId",
                table: "Comments",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
