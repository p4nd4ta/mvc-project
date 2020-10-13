using Microsoft.EntityFrameworkCore.Migrations;

namespace Drinks_Self_Learn.Migrations
{
    public partial class edteddrinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Drinks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
