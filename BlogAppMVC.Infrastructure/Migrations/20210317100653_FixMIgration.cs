using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAppMVC.Infrastructure.Migrations
{
    public partial class FixMIgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
