using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeblogApp.Migrations
{
    public partial class PhotoStringToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
