using Microsoft.EntityFrameworkCore.Migrations;

namespace IsisTravelCore.Data.Migrations
{
    public partial class AlterActivityTableAddImagesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Activities");
        }
    }
}
