using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeblogApp.Migrations
{
    public partial class addKeyBlogCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "blogsCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blogsCategories",
                table: "blogsCategories",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_blogsCategories",
                table: "blogsCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "blogsCategories");
        }
    }
}
