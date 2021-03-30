using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRStest.Migrations
{
    public partial class mig30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UserRating",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingUserId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UserRating",
                table: "Product",
                nullable: true);
        }
    }
}
