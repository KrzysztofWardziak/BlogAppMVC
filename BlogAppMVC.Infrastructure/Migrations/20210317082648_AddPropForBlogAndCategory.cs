using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAppMVC.Infrastructure.Migrations
{
    public partial class AddPropForBlogAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogDetailId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BlogDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_BlogDetailId",
                table: "Photos",
                column: "BlogDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogDetails_CategoryId",
                table: "BlogDetails",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogDetails_Categories_CategoryId",
                table: "BlogDetails",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_BlogDetails_BlogDetailId",
                table: "Photos",
                column: "BlogDetailId",
                principalTable: "BlogDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogDetails_Categories_CategoryId",
                table: "BlogDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_BlogDetails_BlogDetailId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_BlogDetailId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_BlogDetails_CategoryId",
                table: "BlogDetails");

            migrationBuilder.DropColumn(
                name: "BlogDetailId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BlogDetails");
        }
    }
}
