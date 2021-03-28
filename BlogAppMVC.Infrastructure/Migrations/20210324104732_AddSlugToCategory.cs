using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAppMVC.Infrastructure.Migrations
{
    public partial class AddSlugToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_BlogDetails_BlogDetailId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BlogDetailId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BlogDetailId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "BlogDetailId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BlogDetailId",
                table: "Categories",
                column: "BlogDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_BlogDetails_BlogDetailId",
                table: "Categories",
                column: "BlogDetailId",
                principalTable: "BlogDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
