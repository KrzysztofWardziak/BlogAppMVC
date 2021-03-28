using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAppMVC.Infrastructure.Migrations
{
    public partial class AddSlugtoBlogDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogDetails");
        }
    }
}
